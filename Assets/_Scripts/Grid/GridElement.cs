using UnityEngine;

namespace edw.Grid
{
    public class GridElement : MonoBehaviour
    {
        public GridElementType GridElementType;
        public GridOccupier Occupier;

        public GridElement(GridElementType type, GridOccupier occupier)
        {
            GridElementType = type;
            Occupier = occupier;
        }
    }
}
