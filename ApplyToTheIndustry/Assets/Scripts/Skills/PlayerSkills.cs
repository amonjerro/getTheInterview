
using UnityEngine;

public class PlayerSkillsManager : MonoBehaviour
{
    SkillGroup playerSkills;

    private void Awake()
    {
        playerSkills = new SkillGroup(0,0,0,0,0,0,0);
    }

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