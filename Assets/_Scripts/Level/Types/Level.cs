using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace edw.Grids.Levels
{
    public class Level
    {
        public LevelSettings LevelSettings;
        public bool Completed;
        // todo: stats
        public Level(LevelSettings levelSettings, bool completed = false)
        {
            LevelSettings = levelSettings;
        }
    }
}