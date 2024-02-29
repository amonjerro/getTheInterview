using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitButton : MonoBehaviour
{
    // Public fields
    public ResumeComponent resume;
    public ResumeComponent skillPanel;
    public GameObject wastedTimePopup;

    /// <summary>
    /// Submits application to grader and resets
    /// resume on builder
    /// </summary>
    public void OnClick()
    {
        // Add current posting to list of applied positions
        JobManager jobMngr = ServiceLocator.Instance.GetService<JobManager>();
        jobMngr.TryAddAppliedPosition(jobMngr.builderPosting.currentPosting);

        // Check if time was wasted on the application
        // and if so then show popup
        if (jobMngr.playerWastedTime)
        {
            wastedTimePopup.SetActive(true);

            // Reset wasted time status
            jobMngr.playerWastedTime = false;
        }
        else
        {
            // Submit this application to the grader
            ServiceLocator.Instance.GetService<Grader>().OnSubmit(resume as Resume, jobMngr.builderPosting.currentPosting);
        }

        // Reset resume
        resume.Reset();
        skillPanel.Reset();
    }
}
