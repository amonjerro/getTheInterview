using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupButton : MonoBehaviour
{
    /// <summary>
    /// Tells the onboarding manager that it can proceed
    /// when this button is clicked
    /// </summary>
    public void OnClick()
    {
        // Get onboarding manager
        OnboardingManager onboardingMngr = ServiceLocator.Instance.GetService<OnboardingManager>();

        // If the tutorial is still active then proceed with the next tutorial step
        if (onboardingMngr.tutorialActive)
            onboardingMngr.readyProceed = true;
    }
}
