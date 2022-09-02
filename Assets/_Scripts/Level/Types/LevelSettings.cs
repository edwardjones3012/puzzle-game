using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace edw.Grids.Levels
{
    public class LevelSettings : ScriptableObject
    {
        public string LevelName;
        public List<SolutionElement> Solution;
        public string Clue;
        public GridOptions GridOptions;
    }
}