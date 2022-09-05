using edw.Events;
using edw.Grids;
using edw.Grids.Items;
using edw.Grids.Levels;
using edw.Grids.Visuals;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridLogic : MonoBehaviour
{
    Grid<GridElement> grid;

    Vector2 playerPos = new Vector2(2, 0);

    List<Pillar> activePillars = new List<Pillar>();

    [SerializeField] GridElementOccupierVisualiser gridElementOccupierVisualiser;

    private void OnEnable()
    {
        GameEvents.Instance.ChangeLevel.AddDelegate(OnChangeLevel);
    }

    private void OnDisable()
    {
        GameEvents.Instance.ChangeLevel.RemoveDelegate(OnChangeLevel);
    }

    #region Grid Setup
    public void Init(GridOptions gridOptions, List<PillarPosition> startingPositions)
    {
        ResetStates();
        InitGrid(gridOptions);
        SetGridEdgesPlayerExclusive();
        RegisterPillars(startingPositions);
    }

    private void ResetStates()
    {
        gridElementOccupierVisualiser.ResetVisuals();
        activePillars = new List<Pillar>();
        playerPos = new Vector2(2, 0);
    }

    /// <summary>
    /// Creates a new grid and initialises it's elements.
    /// </summary>
    private void InitGrid(GridOptions gridOptions)
    {
        // gridOptions = new GridOptions(5, 5, 2, transform.position);
        // this.gridOptions = gridOptions; 
        grid = new Grid<GridElement>(gridOptions);
        InitialiseGridElements();
        InitialisePlayer();
    }

    /// <summary>
    /// Sets the value of each grid tile to a GridElement.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="occupier"></param>
    private void InitialiseGridElements(GridElementType type = GridElementType.Default, GridOccupier occupier = GridOccupier.None)
    {
        for (int x = 0; x < grid.GridOptions.width; x++)
        {
            for (int y = 0; y < grid.GridOptions.height; y++)
            {
                GridElement gridElement = new GridElement(type, occupier);
                grid.SetValue(x, y, gridElement);
            }
        }
    }

    /// <summary>
    /// Sets each Grid Tile GridElement located on an edge of the grid to be player exclusive.
    /// </summary>
    private void SetGridEdgesPlayerExclusive()
    {
        int width = grid.GridOptions.width;
        int height = grid.GridOptions.height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                {
                    GridElement currentGridElement = grid.GetValue(x, y);
                    currentGridElement.GridElementType = GridElementType.PlayerExclusive;
                    grid.SetValue(x, y, currentGridElement);
                    // Debug.Log(x + ", " + y);
                    // Vector3 halfCell = new Vector3(gridOptions.cellSize, 0, gridOptions.cellSize) / 2;
                    // Debug.DrawLine(grid.GetWorldPosition(x, y) + halfCell, grid.GetWorldPosition(x, y) + halfCell + Vector3.up * 10, Color.white, 10000);
                    // Debug.Log(grid.GetValue(x, y).GridElementType);
                }
            }
        }

        // DebugAllGridElements();
    }
    private void InitialisePlayer()
    {
        grid.GetValue((int)playerPos.x, (int)playerPos.y).Occupier = GridOccupier.Player;
        gridElementOccupierVisualiser.VisualisePlayer(grid.GetWorldPositionCentreGrid((int)playerPos.x, (int)playerPos.y));
    }

    private void DebugAllGridElements()
    {
        int width = grid.GridOptions.width;
        int height = grid.GridOptions.height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Debug.Log(grid.GetValue(x, y).GridElementType);
            }
        }
    }

    private void DebugOccupiedGridElements()
    {
        int width = grid.GridOptions.width;
        int height = grid.GridOptions.height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //if (grid.GetValue(x, y).Occupier != GridOccupier.None)
                //{
                //    Debug.DrawLine(grid.GetWorldPosition(x, y), grid.GetWorldPosition(x, y) + Vector3.up * 10, Color.blue, 2);
                //}
                if (grid.GetValue(x, y).Occupier == GridOccupier.Pillar)
                {
                    Debug.DrawLine(grid.GetWorldPosition(x, y) + new Vector3(grid.GridOptions.cellSize,0, grid.GridOptions.cellSize) /2, grid.GetWorldPosition(x, y) + (Vector3.up * 10) + new Vector3(grid.GridOptions.cellSize, 0, grid.GridOptions.cellSize) / 2, Color.blue, 1);
                }
                if (grid.GetValue(x, y).Occupier == GridOccupier.Player)
                {
                    Debug.DrawLine(grid.GetWorldPosition(x, y) + new Vector3(grid.GridOptions.cellSize, 0, grid.GridOptions.cellSize) / 2, grid.GetWorldPosition(x, y) + (Vector3.up * 10) + new Vector3(grid.GridOptions.cellSize, 0, grid.GridOptions.cellSize) / 2, Color.red, 1);
                }
            }
        }
    }

    /// <summary>
    /// Returns the position of the first tile that has a specified GridElement as it's value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public Vector2? GetPosition(GridElement value)
    {
        for (int x = 0; x < grid.GridOptions.width; x++)
        {
            for (int y = 0; y < grid.GridOptions.height; y++)
            {
                if (value == grid.GetValue(x, y))
                {
                    return new Vector2(x, y);
                }
            }
        }

        return null;
    }

    #endregion

    #region Player Movement

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) TryMove(MoveDirection.Up);
        if (Input.GetKeyDown(KeyCode.A)) TryMove(MoveDirection.Left);
        if (Input.GetKeyDown(KeyCode.S)) TryMove(MoveDirection.Down);
        if (Input.GetKeyDown(KeyCode.D)) TryMove(MoveDirection.Right);
    }

    private void TryMove(MoveDirection moveDir)
    {
        List<GridElement> elementsInDir = GetAllGridElementsUntilUnoccupied(moveDir, playerPos);
        if (elementsInDir.Count > 0)
        {
            GridElement lastElement = elementsInDir[elementsInDir.Count - 1];
            GridElement firstElement = elementsInDir[0];

            if (lastElement.Occupier == GridOccupier.None && lastElement.GridElementType != GridElementType.PlayerExclusive)
            {
                List<GridElement> elementsToShift = elementsInDir.Where(x => x.Occupier != GridOccupier.None).ToList();
                ShiftElements(elementsToShift, moveDir);
                MovePlayer(firstElement);
                DebugOccupiedGridElements();
            }
            else if (lastElement.Occupier == GridOccupier.None && elementsInDir.Count == 1)
            {
                MovePlayer(firstElement);
                DebugOccupiedGridElements();
            }
        }
    }

    private void MovePlayer(GridElement destination)
    {
        grid.GetValue((int)playerPos.x, (int)playerPos.y).Occupier = GridOccupier.None;
        destination.Occupier = GridOccupier.Player;
        Vector2? destPos = GetPosition(destination);
        if (destPos != null)
        {
            playerPos = (Vector2)destPos;
        }
        
        gridElementOccupierVisualiser.MovePlayerObject(grid.GetWorldPositionCentreGrid((int)playerPos.x, (int)playerPos.y));
    }

    void ShiftElements(List<GridElement> elements, MoveDirection moveDir)
    {
        for (int i = elements.Count - 1; i >= 0; i--)
        {
            ShiftElement(elements[i], GetDirectionAsVector2(moveDir));
        }
    }

    void ShiftElement(GridElement element, Vector2 dir)
    {
        Vector2 currentPos;

        currentPos = (Vector2)GetPosition(element);
        if (currentPos == null) Debug.LogError("GetPosition returned null!");

        Vector2 targetPos = currentPos + dir;
        GridElement targetElement = grid.GetValue((int)targetPos.x, (int)targetPos.y);
        if (targetElement == null) Debug.LogError("ShiftElement received invalid target!");

        if (element.Occupier == GridOccupier.Pillar)
        {
            if (TryGetPillar(currentPos, out Pillar pillar))
            {
                MovePillar(pillar, targetPos);
            }
        }

        targetElement.Occupier = element.Occupier;

        if (element.Occupier == GridOccupier.Player)
        {
            playerPos = targetPos;
        }

        element.Occupier = GridOccupier.None;
    }

    private void MovePillar(Pillar pillar, Vector2 targetPos)
    {
        gridElementOccupierVisualiser.MovePillar(pillar, grid.GetWorldPositionCentreGrid((int)targetPos.x, (int)targetPos.y));
        pillar.Position = targetPos;
        GameEvents.Instance.PillarLayoutChanged.Invoke(activePillars);
    }

    private List<GridElement> GetAllGridElementsUntilUnoccupied(MoveDirection moveDir, Vector2 start)
    {
        List<GridElement> elements = new List<GridElement>();

        int loopLimit = GetLoopLimit(moveDir, start);
        Vector2 dir = GetDirectionAsVector2(moveDir);

        for (int i = 0; i < loopLimit; i++)
        {
            GridElement element = GetNextGridElement(dir, playerPos);

            if (element == null)
            {
                // reached the edge
                break;
            }
            else if (element.Occupier == GridOccupier.None)
            {
                // reached as far as we need
                elements.Add(element);
                break;
            }
            else
            {
                // not necessarily reached the end
                elements.Add(element);
                dir += GetDirectionAsVector2(moveDir);
            }
        }

        return elements;
    }

    private Vector2 GetDirectionAsVector2(MoveDirection moveDir)
    {
        if (moveDir == MoveDirection.Up)
        {
            return new Vector2(0, 1);
        }
        if (moveDir == MoveDirection.Down)
        {
            return new Vector2(0, -1);
        }
        if (moveDir == MoveDirection.Left)
        {
            return new Vector2(-1, 0);
        }
        if (moveDir == MoveDirection.Right)
        {
            return new Vector2(1, 0);
        }
        return Vector2.zero;
    }

    private int GetLoopLimit(MoveDirection moveDir, Vector2 start)
    {
        if (moveDir == MoveDirection.Up)
        {
            return grid.GridOptions.height - (int)start.y;
        }
        if (moveDir == MoveDirection.Down)
        {
            return (int)start.y;
        }
        if (moveDir == MoveDirection.Left)
        {
            return (int)start.x;
        }
        if (moveDir == MoveDirection.Right)
        {
            return grid.GridOptions.width - (int)start.x;
        }
        return -1;
    }

    private GridElement GetNextGridElement(Vector2 dir, Vector2 start)
    {
        TryGetElementAtPosition(start + dir, out GridElement element);
        return element;
    }

    private bool TryGetElementAtPosition(Vector2 pos, out GridElement element)
    {
        element = grid.GetValue((int)pos.x, (int)pos.y);
        return element != null;
    }

    #endregion
    #region Pillars

    void RegisterPillars(List<PillarPosition> startingPositions)
    {
        foreach(PillarPosition pillarPosition in startingPositions)
        {
            Pillar p = new Pillar(pillarPosition.GridPosition, pillarPosition.PillarType);
            activePillars.Add(p);
        }

        foreach(Pillar pillar in activePillars)
        {
            GridElement elementAtPos = grid.GetValue((int)pillar.Position.x, (int)pillar.Position.y);
            elementAtPos.Occupier = GridOccupier.Pillar;
            gridElementOccupierVisualiser.VisualisePillar(pillar, grid.GetWorldPositionCentreGrid((int)pillar.Position.x, (int)pillar.Position.y));
            // Debug.Log("Spawning " + pillar.PillarType);
        }
    }

    private bool TryGetPillar(Vector2 pos, out Pillar pillar)
    {
        foreach(Pillar p in activePillars)
        {
            if (p.Position == pos)
            {
                pillar = p;
                return true;
            }
        }
        pillar = default(Pillar);
        return false;
    }
    #endregion

    #region Event Handlers

    private void OnChangeLevel(Level level)
    {
        Init(level.LevelSettings.GridOptions, level.LevelSettings.StartingPositions);
    }

    #endregion
}
