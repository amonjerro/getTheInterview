
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillsManager : MonoBehaviour
{
    public CourseGroupingByDay[] courseGroupings;
    public int studyRate;
    CourseData bookedCourse;
    SkillGroup playerSkills;

    private void Awake()
    {
        playerSkills = new SkillGroup(0,0,0,0,0,0,0);
    }

    public void AddToSkills(SkillGroup trainingReward)
    {
        playerSkills += trainingReward;
    }

    public SkillGroup GetSkills()
    {
        return playerSkills;
    }

    public CourseGroupingByDay GetCoursesByDay(int day)
    {
        return courseGroupings[day];
    }

    public void SetBookedCourse(CourseData data)
    {
        bookedCourse = data;
    }

    public void AdvanceCourse()
    {
        bookedCourse.AddProgress(studyRate);
        if (bookedCourse.isComplete())
        {
            int bonus = bookedCourse.certification ? 2 : 1;
            AddToSkills(bookedCourse.reward * bonus);
        }
    }

    public Skill GetSkillByType(SkillType type)
    {
        return playerSkills.GetSkill(type);
    }
}