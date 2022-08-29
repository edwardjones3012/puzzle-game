using UnityEngine;
namespace edw.Grid
{
    public class Grid<T>
    {
        int width;
        int height;
        int cellSize;
        T[,] gridArray;
        public Grid(int width, int height, int cellSize)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;

            gridArray = new T[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.cyan, 100000);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.cyan, 100000);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.cyan, 100000);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.cyan, 100000);
        }

        private Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, 0, y) * cellSize;
        }

        public void SetValue(int x, int y, T value)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                gridArray[x, y] = value;
                Debug.Log(gridArray[x, y]);
            }
        }

        public T GetValue(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                return gridArray[x, y];
            }
            return default(T);
        }
    }
}
