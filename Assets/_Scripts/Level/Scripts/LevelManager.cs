using edw.Singletons;

namespace edw.Grids.Levels
{
    public class LevelManager : SingletonMonoBehaviour<LevelManager, None>
    {
        int currentLevelIndex;

        public LevelLoader LevelLoader;
        public SolutionWatcher SolutionWatcher;

        void Start()
        {
            Level level = LevelLoader.LoadLevel(0);
            SolutionWatcher.SetActiveSolution(level.LevelSettings.Solution);
        }

        public void LoadNextLevel()
        {
            Level level = LevelLoader.LoadLevel(++currentLevelIndex);
            SolutionWatcher.SetActiveSolution(level.LevelSettings.Solution);
        }

    }
}
