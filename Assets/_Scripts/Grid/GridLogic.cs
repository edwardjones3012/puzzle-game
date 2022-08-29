using edw.Grid;
using UnityEngine;

public class GridLogic : MonoBehaviour
{
    Grid<GridElement> grid;
    GridOptions gridOptions = new GridOptions(5, 5, 2);

    void Start()
    {
        InitGrid();
        SetEdgesPlayerExclusive();
    }

    private void InitGrid()
    {
        grid = new Grid<GridElement>(gridOptions);
        GridElement defaultGridElement = new GridElement(GridElementType.Default, GridOccupier.None);
        grid.SetAllGridElements(defaultGridElement);
    }

    private void SetEdgesPlayerExclusive()
    {
        for (int x = 0; x < gridOptions.width; x++)
        {
            for (int y = 0; y < gridOptions.height; y++)
            {
                if (x == 0)
                {
                    GridElement currentGridElement = grid.GetValue(x, y);
                    currentGridElement.GridElementType = GridElementType.PlayerExclusive;
                    grid.SetValue(x, y, currentGridElement);
                    Debug.Log(grid.GetValue(x, y).GridElementType);
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
