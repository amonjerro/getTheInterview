using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGeneralManager : MonoBehaviour
{
    public List<InterfaceGroup> interfaceGroups;
    public InterfaceGroup feedbackScreen;
    public InterfaceGroup timerPanel;
    public InterfaceGroup gameOverScreen;
    private InterfaceGroup wasActive;
    public InterfaceGroup progressPanel;
    public ConfirmationPopup popUp;
    public Button bookCourseBtn;
    public Button studyCourseBtn;
    private bool isErrorShowing = false;

    private void Start()
    {
        UpdateButtonUsability();
        UpdateCourseUI();
    }
    public void MoveToFeedbackScreen()
    {
        foreach(InterfaceGroup ig in interfaceGroups)
        {
            ig.gameObject.SetActive(false);
        }

        feedbackScreen.gameObject.SetActive(true);
        ClosePopup();
    }

    public void MoveToMainScreen()
    {
        foreach (InterfaceGroup ig in interfaceGroups)
        {
            ig.gameObject.SetActive(false);
        }

        timerPanel.gameObject.SetActive(true);
        if (!ServiceLocator.Instance.GetService<PlayerSkillsManager>().isCourseBooked())
        {
            progressPanel.gameObject.SetActive(false);
        }
        ServiceLocator.Instance.GetService<TimeManager>().ResetTimer();
        ClosePopup();
    }
    
    public void MoveToPauseScreen()
    {
        foreach (InterfaceGroup ig in interfaceGroups)
        {
            if (ig.gameObject.activeInHierarchy)
            {
                wasActive = ig;
            }
            ig.gameObject.SetActive(false);
        }
        ClosePopup();
    }

    public void MoveAwayFromPauseScreen()
    {
        wasActive.gameObject.SetActive(true);
        if (wasActive == timerPanel)
        {
            if (!ServiceLocator.Instance.GetService<PlayerSkillsManager>().isCourseBooked())
            {
                progressPanel.gameObject.SetActive(false);
            }
        }
        ClosePopup();
    }

    public void MoveToGameOverScreen()
    {
        foreach (InterfaceGroup ig in interfaceGroups)
        {
            ig.gameObject.SetActive(false);
        }

        gameOverScreen.gameObject.SetActive(true);
        ClosePopup();
    }

    public void ClosePopup()
    {
        // Get the popup panel and disable if changing interfaces
        HideMenu hiddenPanel = FindObjectOfType<HideMenu>();
        if (hiddenPanel != null)
        {
            // Get job manager to check if time was wasted before disabling
            JobManager jobMngr = ServiceLocator.Instance.GetService<JobManager>();
            if (!jobMngr.playerWastedTime)
                hiddenPanel.DisableObject();
        }
    }

    public void SetProgressPanelStatus(bool status)
    {
        progressPanel.gameObject.SetActive(status);
    }

    public void UpdatePopUp(string message)
    {
        if (isErrorShowing)
        {
            return;
        }
        popUp.SetConfirmationText(message);
    }

    public void ShowPopUp()
    {
        popUp.gameObject.transform.parent.parent.gameObject.SetActive(true);
    }

    public void UpdateButtonUsability()
    {
        // Get the resource manager
        ResourceManager rsrcMngr = ServiceLocator.Instance.GetService<ResourceManager>();

        // Iterate through timer panel buttons
        foreach(Action action in timerPanel.GetComponentsInChildren<Action>())
        {
            // Get game object's button component
            Button btn = action.gameObject.GetComponent<Button>();

            // Check if player can use buttons
            // and disable interactions if they cant
            if (rsrcMngr.IsCostViable(action.cost))
                btn.interactable = true;
            else
                btn.interactable = false;
        }
    }

    public void UpdateErrorStatus(bool status)
    {
        isErrorShowing = status;
    }

    public void UpdateCourseUI()
    {
        // Get the player skills manager
        PlayerSkillsManager psm = ServiceLocator.Instance.GetService<PlayerSkillsManager>();

        // Get whether or not a course is booked
        bool courseBooked = psm.isCourseBooked();

        // Manage UI interactibility based on booking status
        if(courseBooked)
        {
            bookCourseBtn.gameObject.SetActive(false);
            studyCourseBtn.gameObject.SetActive(true);
        }
        else
        {
            bookCourseBtn.gameObject.SetActive(true);
            studyCourseBtn.gameObject.SetActive(false);
        }
        
    }
}
