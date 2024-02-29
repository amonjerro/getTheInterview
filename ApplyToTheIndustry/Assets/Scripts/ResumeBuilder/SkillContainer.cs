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
    private ResumeComponent target;
    private ResumeComponent parent;
    private int index;

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
        target.AddSkill(skill);
        parent.PopSkill(index);
    }

    public void SetTarget(ResumeComponent component)
    {
        target = component;
    }
    public void SetParent(ResumeComponent papa)
    {
        parent = papa;
    }

    public void SetIndex(int value)
    {
        index = value;
    }
}
