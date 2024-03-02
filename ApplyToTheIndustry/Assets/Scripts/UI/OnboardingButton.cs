using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnboardingButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    /// <summary>
    /// Handles click events for proceeding with onboarding tutorial
    /// </summary>
    public void OnClick()
    {
        // Get the onboarding manager
        OnboardingManager onboardingMngr = ServiceLocator.Instance.GetService<OnboardingManager>();

        // Call original button's click events
        onboardingMngr.buttons[onboardingMngr.currentStepIndex].GetComponent<Button>().onClick.Invoke();

        // Advance the onboarding manager to next tutorial step
        onboardingMngr.AdvanceTutorial();

        // Destroy this button
        Destroy(gameObject);
    }
}
