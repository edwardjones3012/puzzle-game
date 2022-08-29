using edw.Grids;
using System.Collections.Generic;
using UnityEngine;

public class GridLogic : MonoBehaviour
{
    Grid<GridElement> grid;
    GridOptions gridOptions;

    Vector2 playerPos;
    Vector2 playerStartingPos = new Vector2(1, 0);

    void Start()
    {
        InitGrid();
        SetGridEdgesPlayerExclusive();
    }

    #region Grid Setup

    private void InitGrid()
    {
        gridOptions = new GridOptions(5, 5, 2, transform.position);
        grid = new Grid<GridElement>(gridOptions);
        GridElement defaultGridElement = new GridElement(GridElementType.Default, GridOccupier.None);
        InitialiseGridElements();
    }

    private void InitialiseGridElements(GridElementType type = GridElementType.Default, GridOccupier occupier = GridOccupier.None)
    {
        for (int x = 0; x < gridOptions.width; x++)
        {
            for (int y = 0; y < gridOptions.height; y++)
            {
                GridElement gridElement = new GridElement(type, occupier);
                grid.SetValue(x, y, gridElement);
            }
        }
    }

    private void SetGridEdgesPlayerExclusive()
    {
        for (int x = 0; x < gridOptions.width; x++)
        {
            for (int y = 0; y < gridOptions.height; y++)
            {
                if (x == 0 || y == 0 || x == gridOptions.width - 1 || y == gridOptions.height - 1)
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

        DebugAllGridElements();
    }

    private void DebugAllGridElements()
    {
        for (int x = 0; x < gridOptions.width; x++)
        {
            for (int y = 0; y < gridOptions.height; y++)
            {
                Debug.Log(grid.GetValue(x, y).GridElementType);
            }
        }
    }

    private void DebugOccupiedGridElements()
    {
        for (int x = 0; x < gridOptions.width; x++)
        {
            for (int y = 0; y < gridOptions.height; y++)
            {
                if (grid.GetValue(x, y).Occupier != GridOccupier.None)
                {
                    Debug.DrawLine(grid.GetWorldPosition(x, y), grid.GetWorldPosition(x, y) + Vector3.up * 10, Color.blue, 2);
                }
            }
        }
    }

    public Vector2? GetPosition(GridElement value)
    {
        for (int x = 0; x < gridOptions.width; x++)
        {
            for (int y = 0; y < gridOptions.height; y++)
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

    #region Game Logic

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

            if (lastElement.Occupier == GridOccupier.None)
            {
                MovePlayer(lastElement);
                DebugOccupiedGridElements();
            }
        }
    }

    private void MovePlayer(GridElement destination)
    {
        // this might not work. might need to make new grid element and setvalue()
        grid.GetValue((int)playerPos.x, (int)playerPos.y).Occupier = GridOccupier.None;
        destination.Occupier = GridOccupier.Player;
        Vector2? destPos = GetPosition(destination);
        if (destPos != null)
        {
            playerPos = (Vector2)destPos;
        }
    }

    private List<GridElement> GetAllGridElementsUntilUnoccupied(MoveDirection moveDir, Vector2 start)
    {
        List<GridElement> elements = new List<GridElement>();

        int loopLimit = 0;
        Vector2 dir = Vector2.zero;

        if (moveDir == MoveDirection.Up)
        {
            loopLimit = gridOptions.height - (int)start.y;
            dir = new Vector2(0, 1);
        }
        if (moveDir == MoveDirection.Down)
        {
            loopLimit = (int)start.y;
            dir = new Vector2(0, -1);
        }
        if (moveDir == MoveDirection.Left)
        {
            loopLimit = (int)start.x;
            dir = new Vector2(-1, 0);
        }
        if (moveDir == MoveDirection.Right)
        {
            loopLimit = gridOptions.width - (int)start.x;
            dir = new Vector2(1, 0);
        }

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
            }
        }

        return elements;
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
}
