using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* CLASS: TimeManager
 * Used for controlling game progression
 * and handling different UI screens
 */
public class TimeManager : MonoBehaviour
{
    // Delegates used for custom event calls
    public delegate void D_Timeout();
    public D_Timeout D_timeout;

    // Store time as integer
    private int currentWeek = 0;
    public int timeLeft = 100;
    private int maxTime;

    // Used for updating timer UI
    public UIBar UIBar;

    // Start is called at beginning of play
    void Start()
    {
        UIBar.Setup();
        maxTime = timeLeft;
    }

    /// <summary>
    /// Used for decreasing timer and invoking tick events
    /// once time is up.
    /// </summary>
    /// <param name="timeSpent">Amount of time being expended</param>
    public void ExpendTime(int timeSpent)
    {
        // Decrease timer
        timeLeft -= timeSpent;
 
        // Check if time is out
        if (timeLeft <= 0)
        {
            // Call tick event
            D_timeout?.Invoke();
        }

        // Make updates to UI
        UIBar.SetFullness(timeLeft /  (float) maxTime);
    }

    /// <summary>
    /// Used for increasing timer
    /// </summary>
    /// <param name="timeAdded">Amount of time being added</param>
    public void AddTime(int timeAdded)
    {
        // Decrease timer
        timeLeft += timeAdded;

        // Make updates to UI
        UIBar.SetFullness(timeLeft / (float)maxTime);
    }

    /// <summary>
    /// Used for resetting time left
    /// </summary>
    public void ResetTimer()
    {
        timeLeft = maxTime;

        // Make updates to UI
        UIBar.SetFullness(timeLeft / (float)maxTime);
    }

    public void SetMaxTime(int value)
    {
        maxTime = value;
    }

    public void EndWeek()
    {
        // Call tick event
        D_timeout?.Invoke();
        currentWeek += 1;
        ResetTimer();
        ServiceLocator.Instance.GetService<UIGeneralManager>().MoveToFeedbackScreen();
    }

    public int GetCurrentWeek()
    {
        return currentWeek;
    }
}
