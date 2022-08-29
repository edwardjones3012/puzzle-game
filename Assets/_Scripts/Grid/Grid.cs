using UnityEngine;
namespace edw.Grids
{
    public class Grid<T>
    {
        GridOptions gridOptions;
        
        T[,] gridArray;

        public Grid(GridOptions gridOptions)
        {
            this.gridOptions = gridOptions;

            gridArray = new T[gridOptions.width, gridOptions.height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.cyan, 100000);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.cyan, 100000);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, gridOptions.height), GetWorldPosition(gridOptions.width, gridOptions.height), Color.cyan, 100000);
            Debug.DrawLine(GetWorldPosition(gridOptions.width, 0), GetWorldPosition(gridOptions.width, gridOptions.height), Color.cyan, 100000);
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, 0, y) * gridOptions.cellSize + gridOptions.offset; // change 0 to custom when offset is made
        }

        public void SetValue(int x, int y, T value)
        {
            if (x >= 0 && y >= 0 && x < gridOptions.width && y < gridOptions.height)
            {
                gridArray[x, y] = value;
            }
        }

        public T GetValue(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < gridOptions.width && y < gridOptions.height)
            {
                return gridArray[x, y];
            }
            return default(T);
        }
    }
}
