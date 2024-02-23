using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExpendTime : MonoBehaviour
{
    // Public fields
    public TimeManager timeMngr;

    /// <summary>
    /// Used for subscribing timer delegate event
    /// </summary>
    void OnEnable()
    {
        timeMngr.D_timeout += TimerOut;
    }

    /// <summary>
    /// Used for unsubscribing timer delegate event
    /// </summary>
    void OnDisable()
    {
        timeMngr.D_timeout -= TimerOut;
    }

    /// <summary>
    /// Used for expending time - mainly for testing purposes in this case
    /// </summary>
    public void SpendTime()
    {
        timeMngr.ExpendTime(12);
    }

    /// <summary>
    /// Method to be called when timer runs out
    /// </summary>
    void TimerOut()
    {
        Debug.Log("Out of time!");
        timeMngr.ResetTimer();
    }
}
