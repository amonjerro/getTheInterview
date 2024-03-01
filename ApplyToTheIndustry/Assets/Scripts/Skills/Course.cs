using System;

[Serializable]
public struct CourseData
{
    public SkillType typeFilter;
    public SkillGroup reward;
    public Resource timeCost;
    public bool certification;
    private int progress;

    public void AddProgress(int value)
    {
        progress += value;
    }

    public bool isComplete()
    {
        return progress >= timeCost.value;
    }
}