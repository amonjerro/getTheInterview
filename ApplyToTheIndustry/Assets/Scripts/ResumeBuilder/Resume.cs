using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/* CLASS: Resume
 * Used as the main object for holding player resume
 * information and updating corresponding UI
 */

public class Resume : ResumeComponent
{
    // Public fields
    public List<Skill> selectedSkillsList;
    public GameObject skillContainerPrefab;
    public float leftPadding;
    public float insetPadding;
    public TextMeshProUGUI resumeTMPSkillList;
    public string resumeMessageDefault;
    public ResumeComponent target;
    public int indexOffset;
    private List<SkillContainer> containers;


    // Start is called before the first frame update
    void Start()
    {
        indexOffset = 3;
        // Init fields here
        selectedSkillsList = new List<Skill>();
        containers = new List<SkillContainer>();

        // Set default list message
        resumeTMPSkillList.text = resumeMessageDefault;
    }

    /// <summary>
    /// Used for adding new skills and updating UI list of skills
    /// </summary>
    /// <param name="skill">Skill being added</param>
    public override void AddSkill(Skill skill)
    {

        GameObject container = Instantiate(skillContainerPrefab);
        // Set it to be a child of this panel
        container.transform.SetParent(transform);
        // Move it to the right place
        RectTransform rt = container.GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, leftPadding, rt.rect.width);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, indexOffset * rt.rect.height + insetPadding, rt.rect.height);

        // Add the skill data
        SkillContainer skillCont = container.GetComponent<SkillContainer>();
        skillCont.SetIndex(indexOffset - 3);
        skillCont.skill = skill;
        skillCont.SetTarget(target);
        skillCont.SetParent(this);
        skillCont.UpdateUIText();
        containers.Add(skillCont);
        indexOffset += 1;

        // Add skill to the list
        selectedSkillsList.Add(skill);
        resumeTMPSkillList.text = "";
    }

    public override void PopSkill(int index)
    {
        indexOffset = 3;
        SkillContainer skillCont = containers[index];
        containers.RemoveAt(index);
        Destroy(skillCont.gameObject);

        // Re-index the list
        foreach (SkillContainer container in containers)
        {
            container.SetIndex(indexOffset - 3);
            RectTransform rt = container.gameObject.GetComponent<RectTransform>();
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, leftPadding, rt.rect.width);
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, indexOffset * rt.rect.height + insetPadding, rt.rect.height);
            indexOffset++;
        }

        if (containers.Count == 0)
        {
            resumeTMPSkillList.text = resumeMessageDefault;
        }
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

