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
    public List<string> selectedSkillsList;
    public TextMeshProUGUI resumeTMPSkillList;
    public string resumeMessageDefault;

    // Start is called before the first frame update
    void Start()
    {
        // Init fields here
        selectedSkillsList = new List<string>();

        // Set default list message
        resumeTMPSkillList.text = resumeMessageDefault;
    }

    /// <summary>
    /// Used for adding new skills and updating UI list of skills
    /// </summary>
    /// <param name="skill">Skill being added</param>
    public void AddSkill(string skill)
    {
        // Add skill to the list
        selectedSkillsList.Add(skill);

        // Update the UI text
        string newText = "";
        foreach(string iSkill in selectedSkillsList)
        {
            newText += "- " + iSkill + "\n";
        }
        resumeTMPSkillList.text = newText;
    }
}
