

using UnityEngine;

public abstract class ResumeComponent : MonoBehaviour
{
    public abstract void AddSkill(Skill skill);
    public abstract void PopSkill(int index);
}