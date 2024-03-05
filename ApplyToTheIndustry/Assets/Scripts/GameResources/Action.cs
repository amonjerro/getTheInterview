using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    public ActionType type;
    private IActionStrategy action;
    ResourceManager rm;
    public InterfaceGroup dependentUI;
    public ActionCost cost;
    public ActionCost undoCost;

    // Start is called before the first frame update
    void Start()
    {
        action = ActionFactory.MakeAction(type);
        rm = ServiceLocator.Instance.GetService<ResourceManager>();
    }

    public void DoAction()
    {
        // Check if cost is viable before doing action
        if(rm.IsCostViable(cost))
        {
            action.Perform(cost);
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

    /// <summary>
    /// Undoes action by returning original action cost to player
    /// </summary>
    public void UndoAction()
    {
        // Call upon resource manager to undo the cost
        ServiceLocator.Instance.GetService<ResourceManager>().UndoCost(undoCost);
    }

}
