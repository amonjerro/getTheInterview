using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Title Screen Behavior - Ported from Twisted Spire, originally developed by Kevin Insinna.
public class TitleScreenBehavior : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject titleScreenUI;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleOptions()
    {
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
        titleScreenUI.SetActive(!titleScreenUI.activeInHierarchy);
    }
}
