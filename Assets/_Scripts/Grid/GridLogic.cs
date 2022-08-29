using edw.Grids;
using UnityEngine;

public class GridLogic : MonoBehaviour
{
    Grid<GridElement> grid;
    GridOptions gridOptions;

    void Start()
    {
        InitGrid();
        SetEdgesPlayerExclusive();
    }

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
    private void SetEdgesPlayerExclusive()
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

    public void DebugAllGridElements()
    {
        for (int x = 0; x < gridOptions.width; x++)
        {
            for (int y = 0; y < gridOptions.height; y++)
            {
                Debug.Log(grid.GetValue(x, y).GridElementType);
            }
        }
    }
}
