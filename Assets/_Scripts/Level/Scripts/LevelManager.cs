using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace edw.Grids.Levels
{
    public class LevelManager : MonoBehaviour
    {
        public LevelLoader LevelLoader;
        public SolutionWatcher SolutionWatcher;

        void Start()
        {
            Level level = LevelLoader.LoadLevel(0);
            SolutionWatcher.SetActiveSolution(level.LevelSettings.Solution);
        }
    }
}