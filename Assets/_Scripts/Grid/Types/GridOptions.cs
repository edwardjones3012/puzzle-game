using UnityEngine;

[System.Serializable]
public struct GridOptions 
{
    public int width;
    public int height;
    public int cellSize;
    public Vector3 offset;

    public GridOptions(int width, int height, int cellSize, Vector3 offset)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.offset = offset;
    }
}
