using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Pause Screen Behavior - Adapted from Twisted Spire, originally developed by Kevin Insinna
public class PauseScreenBehavior : MonoBehaviour
{
    bool isPaused = false;
    public GameObject pauseScreen;

    private void Update()
    {
        //Pause when Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    //Toggles the game being paused
    public void PauseGame()
    {
        isPaused = !isPaused;
        
        if (isPaused)
        {
            ServiceLocator.Instance.GetService<UIGeneralManager>().MoveToPauseScreen();
            //Toggle pause UI on and game UI off
            pauseScreen.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }

        else
        {
            ServiceLocator.Instance.GetService<UIGeneralManager>().MoveAwayFromPauseScreen();
            //Toggle pause UI off and game UI on
            pauseScreen.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    //Goes back to Title Screen
    public void BackToMainMenu()
    {
        // To do: Need to add some sort of reset functionality here

        SceneManager.LoadScene("TitleScreen");
    }
}
