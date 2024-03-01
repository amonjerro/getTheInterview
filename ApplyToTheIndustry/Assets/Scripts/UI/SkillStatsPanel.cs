using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStatsPanel : MonoBehaviour
{
    public List<UIBar> skillBars;
    public List<SkillType> skillTypes;
    private bool setupComplete = false;

    private void OnEnable()
    {
        PlayerSkillsManager psm = ServiceLocator.Instance.GetService<PlayerSkillsManager>();
        for(int i = 0; i < skillTypes.Count; i++)
        {
            if (!setupComplete)
            {
                skillBars[i].Setup();
            }
            Skill playerSkill = psm.GetSkillByType(skillTypes[i]);
            float skillFullness = Mathf.Clamp(playerSkill.value / 100.0f, 0.01f, 1.0f);
            skillBars[i].SetFullness(skillFullness);
        }
        setupComplete = true;
    }
}
