using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    // Public fields
    public PostingsByDay postingDay1;
    public JobContainer builderPosting;

    /// <summary>
    /// Sets the current posting the player will be interacting with
    /// </summary>
    /// <param name="posting">Posting being set as current</param>
    public void SetCurrentPosting(JobPosting posting)
    {
        builderPosting.currentPosting = posting;
        builderPosting.UpdateUI();
    }
}
