
using UnityEngine;

public class PlayerSkillsManager : MonoBehaviour
{
    SkillGroup playerSkills;

    public void SetupSkills(){
        playerSkills = new SkillGroup();
    }

    public void AddToSkills(SkillGroup trainingReward)
    {
        playerSkills += trainingReward;
    }

    public SkillGroup GetSkills()
    {
        return playerSkills;
    }
}