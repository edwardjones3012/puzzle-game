using edw.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace edw.Grids.Levels
{
    public class LevelLoader : MonoBehaviour
    {
        public List<LevelSettings> LevelReference = new List<LevelSettings>();
        List<Level> Levels = new List<Level>();
        // [SerializeField] GridLogic gridLogic;
        bool levelsInitialised { get { return Levels.Count > 0; } set { } }

        void Start()
        {
            Init();
        }

        void Init()
        {
            if (levelsInitialised)
            {
                Debug.LogWarning("Levels already initialised!");
                return;
            }

            foreach(LevelSettings levelSettings in LevelReference)
            {
                Levels.Add(new Level(levelSettings));
            }
            levelsInitialised = true;
        }

        public Level LoadLevel(int levelIndex)
        {
            if (levelIndex > LevelReference.Count - 1 || levelIndex < 0)
            {
                Debug.LogError("Invalid level index!");
            }

            LevelSettings levelSettings = LevelReference[levelIndex];
            // gridLogic.Init(levelSettings.GridOptions, levelSettings.StartingPositions);
            GameEvents.Instance.ChangeLevel.Invoke(GetLevel(levelSettings));

            Debug.Log("Loading level: " + levelSettings.LevelName + "!");
            return GetLevel(levelSettings);
        }

        private Level GetLevel(LevelSettings reference)
        {
            foreach(Level level in Levels)
            {
                if (level.LevelSettings == reference) return level;
            }
            return null;
        }

        public Level GetLevel(int index)
        {
            return Levels[index];
        }
    }
}
