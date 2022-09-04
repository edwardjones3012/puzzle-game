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
            if (currentLevelIndex == LevelLoader.LevelReference.Count - 1) return;
            Level level = LevelLoader.LoadLevel(++currentLevelIndex);
            SolutionWatcher.SetActiveSolution(level.LevelSettings.Solution);
        }

        public Level GetCurrentLevel()
        {
            return LevelLoader.GetLevel(currentLevelIndex);
        }

        public ProgressionAction GetProgressionAction()
        {
            if (currentLevelIndex == LevelLoader.LevelReference.Count - 1)
            {
                return ProgressionAction.ReturnToMenu;
            }
            return ProgressionAction.NextLevel;
        }
    }

    public enum ProgressionAction
    {
        NextLevel,
        ReturnToMenu
    }
}
