using UnityEngine;

namespace edw.Grids
{
    public struct GridElement
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
