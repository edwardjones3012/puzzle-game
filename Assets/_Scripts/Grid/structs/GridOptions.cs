using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridOptions 
{
    public int width;
    public int height;
    public int cellSize;

    public GridOptions(int width, int height, int cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
    }
}
