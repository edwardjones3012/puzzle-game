using edw.Grid;
using UnityEngine;

public class GridLogic : MonoBehaviour
{
    void Start()
    {
        Grid<GridElement> grid = new Grid<GridElement>(5, 5, 2);
    }
}
