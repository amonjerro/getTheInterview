using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* CLASS: GameOverScreenBehavior
 * Used for defining button behaviors for
 * game over screen
 */
public class GameOverScreenBehavior : MonoBehaviour
{
    /// <summary>
    /// Restarts the main game scene
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Sends player back to main menu
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Quits the game app
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
