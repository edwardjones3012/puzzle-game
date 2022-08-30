using UnityEngine;

namespace edw.Grids.Items
{
    public struct Pillar 
    {
        public Pillar(Vector2 Position, PillarType PillarType)
        {
            this.Position = Position;
            this.PillarType = PillarType;
        }
        public Vector2 Position;
        public PillarType PillarType;
    }   
}
