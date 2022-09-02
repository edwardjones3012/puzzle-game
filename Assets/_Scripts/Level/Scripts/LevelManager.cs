using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace edw.Grids.Levels
{
    public class LevelManager : MonoBehaviour
    {
        public LevelLoader LevelLoader;

        void Start()
        {
            LevelLoader.LoadLevel(0);
        }
    }
}