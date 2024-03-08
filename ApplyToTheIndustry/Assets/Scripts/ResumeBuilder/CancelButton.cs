using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CancelButton : MonoBehaviour
{
    public ResumeComponent resume;
    public ResumeComponent skillPanel;

     public Button cancelButton;
    public GameObject blackoutPanet;

    

    public void Update()
    {
        
        OnboardingManager onboardingMngr = ServiceLocator.Instance.GetService<OnboardingManager>();
        Debug.Log(onboardingMngr.tutorialActive);
        if(onboardingMngr != null)
        {
            
            // Only proceed if currently in tutorial
            if(onboardingMngr.tutorialActive)
            {
                if(blackoutPanet.activeSelf)
                {
                    cancelButton.interactable = true;
                }
                else
                {
                    cancelButton.interactable = false;
                }
            }
            else
            {
                cancelButton.interactable = true;
            }
        }

    }

    public void OnClick()
    {
        UIGeneralManager ugm = ServiceLocator.Instance.GetService<UIGeneralManager>();
        ugm.UpdatePopUp("Your application has been cancelled!");
        ugm.ShowPopUp();

         // Reset resume
        resume.Reset();
        skillPanel.Reset();
        
    }
}
