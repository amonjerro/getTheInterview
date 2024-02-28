using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/* CLASS: Resume
 * Used as the main object for holding player resume
 * information and updating corresponding UI
 */

public class Resume : MonoBehaviour
{
    // Public fields
    public List<Skill> selectedSkillsList;
    public TextMeshProUGUI resumeTMPSkillList;
    public string resumeMessageDefault;

    // Onboarding fields
    public int neededClicks = 3;
    public int tutorialClicks = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Init fields here
        selectedSkillsList = new List<Skill>();

        // Set default list message
        resumeTMPSkillList.text = resumeMessageDefault;
    }

    /// <summary>
    /// Used for adding new skills and updating UI list of skills
    /// </summary>
    /// <param name="skill">Skill being added</param>
    public void AddSkill(Skill skill)
    {
        // Add skill to the list
        selectedSkillsList.Add(skill);

        // Update the UI text
        string newText = "";
        foreach(Skill iSkill in selectedSkillsList)
        {
            newText += "- " + iSkill.name + "\n";
        }
        resumeTMPSkillList.text = newText;
    }

    /// <summary>
    /// Resets builder resume
    /// </summary>
    public void ResetResume()
    {
        // Clear list and update UI
        selectedSkillsList.Clear();
        resumeTMPSkillList.text = "";
    }
}

