using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobPostingDayPanel : MonoBehaviour
{
    // Public fields
    public GameObject jobButtonPrefab;
    public InterfaceGroup dependentUI;

    /// <summary>
    /// Once enabled take all of the current day job postings
    /// and display them on the GUI for the player to select
    /// </summary>
    void OnEnable()
    {
        // Get the job manager and all job postings from current day
        JobManager jobMngr = ServiceLocator.Instance.GetService<JobManager>();
        PostingsByDay currentDayJobs = jobMngr.postingDay1;
        JobPosting[] listOfJobs = currentDayJobs.posts;

        // Get the grid layout group element and add all job buttons to it
        GridLayoutGroup layout = GetComponentInChildren<GridLayoutGroup>();
        foreach (JobPosting job in listOfJobs)
        {
            // Create the button
            GameObject jobButton = Instantiate(jobButtonPrefab);

            // Set it to be a child of the grid layout
            jobButton.transform.SetParent(layout.transform);

            // Add the job data
            JobContainer jobCont = jobButton.GetComponent<JobContainer>();
            jobCont.currentPosting = job;
            jobCont.UpdateUI();

            // Set action UI
            Action action = jobButton.GetComponent<Action>();
            action.dependentUI = dependentUI;
        }
    }

    /// <summary>
    /// Clean up the grid every time it gets disabled
    /// </summary>
    void OnDisable()
    {
        // Get the grid layout and clear all objects within the grid
        GridLayoutGroup layout = GetComponentInChildren<GridLayoutGroup>();
        for(int i = 0; i < layout.transform.childCount; i++)
        {
            Destroy(layout.transform.GetChild(i).gameObject);
        }
    }
}
