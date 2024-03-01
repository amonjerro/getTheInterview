using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitButton : MonoBehaviour
{
    // Public fields
    public ResumeComponent resume;
    public ResumeComponent skillPanel;

    /// <summary>
    /// Submits application to grader and resets
    /// resume on builder
    /// </summary>
    public void OnClick()
    {
        // Add current posting to list of applied positions
        JobManager jobMngr = ServiceLocator.Instance.GetService<JobManager>();
        jobMngr.TryAddAppliedPosition(jobMngr.builderPosting.currentPosting);
        UIGeneralManager ugm = ServiceLocator.Instance.GetService<UIGeneralManager>();
        // Enable the popup
        ugm.ShowPopUp();

        // Check if time was wasted on the application
        // and if so then change popup text
        if (jobMngr.playerWastedTime)
        {
            // Set popup text
            ugm.UpdatePopUp("Sorry! You have already applied here. We only allow one application per person.");

            // Reset wasted time status
            jobMngr.playerWastedTime = false;
        }
        else
        {
            // Set popup text
            ugm.UpdatePopUp("Your application has been sent!");

            // Submit this application to the grader
            ServiceLocator.Instance.GetService<Grader>().OnSubmit(resume as Resume, jobMngr.builderPosting.currentPosting);
        }

        // Get onboarding manager
        OnboardingManager onboardingMngr = ServiceLocator.Instance.GetService<OnboardingManager>();
        if(onboardingMngr != null)
        {
            // Only proceed if currently in tutorial
            if(onboardingMngr.tutorialActive)
            {
                // If current step in onboarding wasnt this then skip
                // the submit instructions
                if (onboardingMngr.buttons[onboardingMngr.currentStepIndex] != gameObject)
                {
                    onboardingMngr.readyProceed = true;
                    onboardingMngr.currentStepIndex++;
                    onboardingMngr.AdvanceTutorial();

                    // Also set resume clicks to whats needed
                    resume.tutorialClicks = resume.neededClicks;
                }
            }
        }

        // Reset resume
        resume.Reset();
        skillPanel.Reset();
    }
}
