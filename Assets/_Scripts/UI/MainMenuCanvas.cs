using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCanvas : MonoBehaviour
{
    public void OnPlayClicked()
    {
        SceneManager.LoadScene(1);
    }
}
