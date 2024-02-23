
using UnityEngine;

public class PlayerSkillsManager : MonoBehaviour
{
    SkillGroup playerSkills;
    public static PlayerSkillsManager Instance;

    public static void SetUpInstance(){
        Instance = new PlayerSkillsManager();
        Instance.playerSkills = new SkillGroup();
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
        if (playerSkills == null)
        {
            SetupSkills();
        }
        return playerSkills;
    }
}