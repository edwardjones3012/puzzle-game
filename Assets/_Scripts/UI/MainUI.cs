using edw.Events;
using edw.Grids.Levels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] private TMP_Text clueText;
    [SerializeField] private GameObject nextLevelButton;

    void Start()
    {
        GameEvents.Instance.ChangeLevel.AddDelegate(OnChangeLevel);
        GameEvents.Instance.CorrectConfigurationMade.AddDelegate(OnLevelComplete);
    }

    private void OnLevelComplete()
    {
        nextLevelButton.SetActive(true);
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
}
