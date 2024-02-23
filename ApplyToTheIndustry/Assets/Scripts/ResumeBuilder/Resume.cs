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

     public static Resume Instance;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
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
            newText += "- " + iSkill.Name + "\n";
        }
        resumeTMPSkillList.text = newText;
    }
}

