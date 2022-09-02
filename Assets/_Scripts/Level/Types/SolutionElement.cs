using edw.Grids.Items;
using UnityEngine;

namespace edw.Grids.Levels
{
    [System.Serializable]
    public struct SolutionElement
    {
        public bool RequireType;
        public PillarType PillarType;
        public Vector2 GridPosition;
    }   
}