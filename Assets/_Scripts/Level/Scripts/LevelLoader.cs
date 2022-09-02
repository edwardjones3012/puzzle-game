using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace edw.Grids.Levels
{
    public class LevelLoader : MonoBehaviour
    {
        public List<Level> Levels = new List<Level>();

        public void LoadLevel(int levelIndex)
        {
            if (levelIndex > Levels.Count - 1 || levelIndex < 0)
            {
                Debug.LogError("Invalid level index!");
            }

            Debug.Log("Loading level: " + Levels[levelIndex].LevelSettings.LevelName + "!");
        }
    }
}
