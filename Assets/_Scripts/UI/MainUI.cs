using edw.Events;
using edw.Grids.Levels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    [SerializeField] private TMP_Text clueText;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject returnToMenuButton;

    void Start()
    {
        GameEvents.Instance.ChangeLevel.AddDelegate(OnChangeLevel);
        GameEvents.Instance.CorrectConfigurationMade.AddDelegate(OnLevelComplete);
    }

    private void OnLevelComplete()
    {
        if (LevelManager.Instance.GetProgressionAction() == ProgressionAction.NextLevel)
        {
            nextLevelButton.SetActive(true);
        }
        if (LevelManager.Instance.GetProgressionAction() == ProgressionAction.ReturnToMenu)
        {
            returnToMenuButton.SetActive(true);
        }
    }

    private void OnChangeLevel(Level level)
    {
        clueText.text = level.LevelSettings.Clue;
    }

    public void OnNextLevelButtonClicked()
    {
        LevelManager.Instance.LoadNextLevel();
        nextLevelButton.SetActive(false);
    }

    public void OnGoToMainMenuButtonClicked()
    {
        SceneManager.LoadScene(0);
        returnToMenuButton.SetActive(false);
    }
}
