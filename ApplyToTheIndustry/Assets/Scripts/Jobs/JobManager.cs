using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    // Public fields
    public PostingsByDay[] postingsByDayContainer;
    public JobContainer builderPosting;
    public bool playerWastedTime = false;

    // Private fields
    List<JobPosting> appliedPositions = new List<JobPosting>();

    /// <summary>
    /// Sets the current posting the player will be interacting with
    /// </summary>
    /// <param name="posting">Posting being set as current</param>
    public void SetCurrentPosting(JobPosting posting)
    {
        builderPosting.currentPosting = posting;
        builderPosting.UpdateUI();
    }

    /// <summary>
    /// Attempt to add a given posting to the list of applications
    /// and if it already exists then mark player as wasting their time
    /// </summary>
    /// <param name="posting">Posting trying to be added</param>
    public void TryAddAppliedPosition(JobPosting posting)
    {
        // Check if posting is already in list of applied positions
        // and if it is mark wasted time
        if (appliedPositions.Contains(posting))
        {
            playerWastedTime = true;
            return;
        }

        // Otherwise add to list of applied positions
        appliedPositions.Add(posting);
    }

    public PostingsByDay GetByDay(int i)
    {
        int index = i;
        if (i >= postingsByDayContainer.Length)
        {
            index = 0;
        }
        return postingsByDayContainer[index];
    }
}
