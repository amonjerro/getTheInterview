using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* CLASS: SkillPanel
 * Used for managing pool of options for Resume Builder
 */
public class SkillPanel : MonoBehaviour
{
    // Public fields
    public GameObject skillContainerPrefab;
    public float insetPadding;
    public float leftPadding;
    public Resume resume;

    //public List<SkillContainer> skillOptions;

    void Awake()
    {

    }
    /// <summary>
    /// Called upon activation of UI panel - should populate skill containers dynamically
    /// </summary>
    void OnEnable()
    {
        int indexOffset = 3;
        if (PlayerSkillsManager.Instance == null){
            PlayerSkillsManager.SetUpInstance();
        }
        SkillGroup playerSkills = PlayerSkillsManager.Instance.GetSkills();
        List<Skill> listOfPlayerSkills = playerSkills.ListSkills();
        foreach(Skill skill in listOfPlayerSkills)
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
            skillCont.skill = skill;
            skillCont.currentResume = resume;
            skillCont.UpdateUIText();
            indexOffset += 1;
        }
        
    }
}
