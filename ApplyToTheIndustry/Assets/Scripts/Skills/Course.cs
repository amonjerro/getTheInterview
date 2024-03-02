using System;
using UnityEngine;

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

    public float GetProgressPercent()
    {
        return Mathf.Clamp(progress / (float)timeCost.value, 0, 1);
    }

    public bool isComplete()
    {
        return progress >= timeCost.value;
    }
}