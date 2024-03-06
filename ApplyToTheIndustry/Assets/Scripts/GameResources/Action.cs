using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Action : MonoBehaviour
{
    public ActionType type;
    private IActionStrategy action;
    ResourceManager rm;
    public InterfaceGroup dependentUI;
    public ActionCost cost;
    public ActionCost undoCost;
    GameObject hoverTip;

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
        uiMngr.CloseHoverTips();
    }

    /// <summary>
    /// Undoes action by returning original action cost to player
    /// </summary>
    public void UndoAction()
    {
        // Call upon resource manager to undo the cost
        ServiceLocator.Instance.GetService<ResourceManager>().UndoCost(undoCost);
    }

    public void ShowActionCost()
    {
        // Get where mouse is currently located
        Vector3 hoverLoc = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // Get the UI manager
        UIGeneralManager uiMngr = ServiceLocator.Instance.GetService<UIGeneralManager>();

        // Hover position offset
        Vector3 posOffset = new Vector3(-gameObject.GetComponentInChildren<RectTransform>().rect.size.x, 0.0f, 0.0f);

        // Create instance of hover tip and set its position to hover location
        hoverTip = Instantiate(uiMngr.hoverTipRef, transform);
        hoverTip.transform.SetParent(transform, false);
        hoverTip.transform.localPosition = hoverLoc + posOffset;

        // Also set text to be relative to action cost
        hoverTip.GetComponentInChildren<TextMeshProUGUI>().text = "Time Cost: " + cost.time.value + "\tMoney Cost: " + cost.money.value;
    }

    public void HideActionCost()
    {
        Destroy(hoverTip);
    }

}
