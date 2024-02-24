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
        skillTMPTitle.text = skill.Name;
    }

    /// <summary>
    /// Called upon clicking container button
    /// </summary>
    public void OnChoose()
    {
        // Add skill to player resume
        currentResume.AddSkill(skill);
    }
}
