using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OnboardingManager : MonoBehaviour
{
    // Public fields
    public GameObject[] buttons;
    public GameObject[] instructions;
    public int currentStepIndex = 0;
    public GameObject blackOutPanel;
    public bool tutorialActive = true;
    public bool readyProceed = false;

    // Private fields
    bool buttonInGrid;

    // Start is called before the first frame update
    void Start()
    {
        // Enable the first tutorial step
        if(tutorialActive)
            EnableTutorialStep();
    }

    /// <summary>
    /// Enables tutorial button and instructions
    /// for the player to get context
    /// </summary>
    public void EnableTutorialStep()
    {
        // Enable blackout panel and instructions panel
        blackOutPanel.SetActive(true);
        instructions[currentStepIndex].SetActive(true);

        // Instantiate the button of focus and
        // set its position 
        GameObject highlightButton = Instantiate(buttons[currentStepIndex], buttons[currentStepIndex].transform);
        highlightButton.gameObject.transform.SetParent(blackOutPanel.transform);
        RectTransform rect = buttons[currentStepIndex].GetComponent<RectTransform>();
        highlightButton.transform.position = rect.position;
        if (buttonInGrid) // Handle positioning different for buttons in a grid
        {
            highlightButton.transform.position = Camera.main.WorldToScreenPoint(rect.position);
            buttonInGrid = false;
        }

        // Remove unused components
        Destroy(highlightButton.GetComponent<Action>());
        Destroy(highlightButton.GetComponent<JobButton>());
        Destroy(highlightButton.GetComponent<SubmitButton>());
        Destroy(highlightButton.GetComponent<SkillContainer>());

        // Add onboarding button component
        highlightButton.AddComponent<OnboardingButton>();

        // Ensure highlighted button is active
        highlightButton.SetActive(true);
    }

    /// <summary>
    /// Advances tutorial to the next step
    /// </summary>
    public void AdvanceTutorial()
    {
        // Return if not yet ready to proceed
        if (!readyProceed)
            return;

        // Deactivate the panels and increment step index
        blackOutPanel.SetActive(false);
        instructions[currentStepIndex].SetActive(false);
        currentStepIndex++;

        // Return early if next index is out of bounds
        if (currentStepIndex >= buttons.Length)
        {
            tutorialActive = false;
            return;
        }
        
        // Handle grid structures
        GridLayoutGroup grid = buttons[currentStepIndex].gameObject.GetComponent<GridLayoutGroup>();
        if (grid != null)
        {
            buttons[currentStepIndex] = grid.transform.GetChild(0).gameObject;
            buttonInGrid = true;
        }

        // Handle skills panel
        SkillPanel skillsPanel = buttons[currentStepIndex].gameObject.GetComponent<SkillPanel>();
        if(skillsPanel != null)
        {
            buttons[currentStepIndex] = skillsPanel.transform.GetChild(0).gameObject;
        }

        // Enable the next tutorial step
        EnableTutorialStep();

        // Reset ready flag
        readyProceed = false;
    }

    /// <summary>
    /// Disables current instructions
    /// </summary>
    public void DisableInstructions()
    {
        blackOutPanel.SetActive(false);
        instructions[currentStepIndex].SetActive(false);
        
    }
}
