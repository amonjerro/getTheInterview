using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/* CLASS: SkillContainer
 * Used for holding information on any individual skill
 * that'll be used to add to player resume
 */
public class SkillContainer : MonoBehaviour
{
    // Public fields
    public TextMeshProUGUI skillTMPTitle;
    public Skill skill;
    public Resume currentResume;

    /// <summary>
    /// Used for updating UI text with
    /// information from skill
    /// </summary>
    public void UpdateUIText()
    {
        skillTMPTitle.text = skill.name;
    }

    /// <summary>
    /// Called upon clicking container button
    /// </summary>
    public void OnChoose()
    {
        // Add skill to player resume
        currentResume.AddSkill(skill);

        // Update onboarding manager
        // Get onboarding manager
        OnboardingManager onboardingMngr = ServiceLocator.Instance.GetService<OnboardingManager>();

        // If the tutorial is still active then proceed with the next tutorial step
        if(onboardingMngr != null)
        {
            if (onboardingMngr.tutorialActive)
            {
                // Disable instructions to let multiple clicks
                onboardingMngr.DisableInstructions();

                // Increase click counter and advance tutorial when ready
                currentResume.tutorialClicks++;
                if (currentResume.tutorialClicks == currentResume.neededClicks)
                {
                    onboardingMngr.readyProceed = true;
                    onboardingMngr.AdvanceTutorial();
                }
            }
        }
    }
}
