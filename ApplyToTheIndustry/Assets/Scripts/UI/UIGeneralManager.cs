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

    private void Start()
    {
        ServiceLocator.Instance.GetService<TimeManager>().D_timeout += MoveToFeedbackScreen;
        UpdateButtonUsability();
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

}
