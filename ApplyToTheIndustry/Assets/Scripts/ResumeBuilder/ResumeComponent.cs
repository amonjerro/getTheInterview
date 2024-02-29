

using UnityEngine;

public abstract class ResumeComponent : MonoBehaviour
{
    // Onboarding fields
    public int neededClicks;
    public int tutorialClicks;
    public abstract void AddSkill(Skill skill);
    public abstract void PopSkill(int index);
}