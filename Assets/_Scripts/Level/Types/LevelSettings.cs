using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace edw.Grids.Levels
{
    [CreateAssetMenu(menuName = "Levels/Level Settings", fileName = "Level Settings")]
    public class LevelSettings : ScriptableObject
    {
        public string LevelName;
        public string Clue;
        public GridOptions GridOptions;
        public List<SolutionElement> Solution;
    }
}