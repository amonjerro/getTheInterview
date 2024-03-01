using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Action : MonoBehaviour
{
    ResourceManager rm;
    public InterfaceGroup dependentUI;
    public ActionCost cost;
    Grader feedback;

    // Start is called before the first frame update
    void Start()
    {
        rm = ServiceLocator.Instance.GetService<ResourceManager>();
        feedback = ServiceLocator.Instance.GetService<Grader>();
    }

    public void DoAction()
    {
        // Check if cost is viable before doing action
        if(rm.IsCostViable(cost))
        {
            rm.ManageCost(cost);
        }

        // Get the general UI manager
        UIGeneralManager uiMngr = ServiceLocator.Instance.GetService<UIGeneralManager>();

        // Update button usability
        uiMngr.UpdateButtonUsability();
    }

    public void OpenInterface()
    {
        DoAction();
        dependentUI.gameObject.SetActive(true);
        gameObject.GetComponentInParent<InterfaceGroup>().gameObject.SetActive(false);

        // Get onboarding manager
        OnboardingManager onboardingMngr = ServiceLocator.Instance.GetService<OnboardingManager>();

        // If the tutorial is still active then proceed with the next tutorial step
        if(onboardingMngr != null)
            if (onboardingMngr.tutorialActive)
                onboardingMngr.readyProceed = true;
                
        
        // Get UI general manager and call close popup
        UIGeneralManager uiMngr = ServiceLocator.Instance.GetService<UIGeneralManager>();
        uiMngr.ClosePopup();
    }

}
