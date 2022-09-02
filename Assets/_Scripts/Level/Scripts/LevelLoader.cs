using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace edw.Grids.Levels
{
    public class LevelLoader : MonoBehaviour
    {
        public List<LevelSettings> LevelReference = new List<LevelSettings>();
        List<Level> Levels = new List<Level>();

        [SerializeField] GridLogic gridLogic;
        public void LoadLevel(int levelIndex)
        {
            if (levelIndex > LevelReference.Count - 1 || levelIndex < 0)
            {
                Debug.LogError("Invalid level index!");
            }

            LevelSettings levelSettings = LevelReference[levelIndex];
            gridLogic.Init(levelSettings.GridOptions);

            Debug.Log("Loading level: " + levelSettings.LevelName + "!");
        }
    }
}
