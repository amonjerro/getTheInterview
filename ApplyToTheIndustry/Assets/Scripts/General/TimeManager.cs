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
    public int timeLeft = 100;

    // Used for updating timer UI
    public TextMeshProUGUI timerTMP;

    // Start is called at beginning of play
    void Start()
    {
        timerTMP.text = timeLeft.ToString();
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
        timerTMP.text = timeLeft.ToString();
    }

    /// <summary>
    /// Used for resetting time left
    /// </summary>
    public void ResetTimer()
    {
        timeLeft = 100;
    }
}
