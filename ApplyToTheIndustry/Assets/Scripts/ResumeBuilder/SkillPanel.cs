using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* CLASS: SkillPanel
 * Used for managing pool of options for Resume Builder
 */
public class SkillPanel : ResumeComponent
{
    // Public fields
    public GameObject skillContainerPrefab;
    public float insetPadding;
    public float leftPadding;
    public ResumeComponent resume;
    public int indexOffset;
    List<SkillContainer> skillContainers = new List<SkillContainer>();


    void OnEnable()
    {
        indexOffset = 3;
        PlayerSkillsManager psm = ServiceLocator.Instance.GetService<PlayerSkillsManager>();
        SkillGroup playerSkills = psm.GetSkills();
        List<Skill> listOfPlayerSkills = playerSkills.ListSkills();
        foreach (Skill skill in listOfPlayerSkills)
        {
            AddSkill(skill);
        }
    }

    public override void PopSkill(int index)
    {
        indexOffset = 3;
        if (index >= skillContainers.Count)
        {
            Debug.Log("An index lookup is out of range in the Skill Panel");
            Debug.Log(skillContainers.Count);
            Debug.Log(index);
            return;
        }
        SkillContainer skillCont = skillContainers[index];
        skillContainers.RemoveAt(index);
        Destroy(skillCont.gameObject);

        // Re-index the list
        foreach(SkillContainer container in skillContainers)
        {
            container.SetIndex(indexOffset - 3);
            RectTransform rt = container.gameObject.GetComponent<RectTransform>();
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, leftPadding, rt.rect.width);
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, indexOffset * rt.rect.height + insetPadding, rt.rect.height);
            indexOffset++;
        }
    }

    public override void AddSkill(Skill skill)
    {

        // Create the container
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
        skillCont.SetParent(this);
        skillCont.SetTarget(resume);
        skillCont.UpdateUIText();
        skillContainers.Add(skillCont);
        indexOffset += 1;
    }

    public override void Reset()
    {
        skillContainers.Clear();
    }

}
