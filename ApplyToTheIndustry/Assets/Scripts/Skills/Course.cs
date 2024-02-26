using System;

[Serializable]
public struct CourseData
{
    public string courseName;
    public SkillType typeFilter;
    public SkillGroup reward;
    public Resource moneyCost;
    public Resource timeCost;
    public bool certification;
}