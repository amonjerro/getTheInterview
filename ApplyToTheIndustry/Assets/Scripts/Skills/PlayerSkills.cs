
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillsManager : MonoBehaviour
{
    public CourseGroupingByDay[] courseGroupings;
    [SerializeField]
    private int studyRate =  10;
    private bool courseIsBooked = false;
    public UIBar progressBar;
    CourseData bookedCourse;
    SkillGroup playerSkills;

    private void Awake()
    {
        playerSkills = new SkillGroup(0,0,0,0,0,0,0);
        progressBar.Setup();
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
        courseIsBooked = true;
        progressBar.SetFullness(0);
        ServiceLocator.Instance.GetService<UIGeneralManager>().SetProgressPanelStatus(courseIsBooked);
    }

    public bool isCourseBooked()
    {
        return courseIsBooked;
    }

    public void AdvanceCourse()
    {
        if (!courseIsBooked)
        {
            return;
        }

        bookedCourse.AddProgress(studyRate);
        float proportion = bookedCourse.GetProgressPercent();
        progressBar.SetFullness(proportion);
        if (bookedCourse.isComplete())
        {
            int bonus = bookedCourse.certification ? 2 : 1;
            AddToSkills(bookedCourse.reward * bonus);
            courseIsBooked = false;
            UIGeneralManager uigm = ServiceLocator.Instance.GetService<UIGeneralManager>();
            uigm.SetProgressPanelStatus(courseIsBooked);
            uigm.ShowPopUp();
            uigm.UpdatePopUp("You have finished studying your course! Check your profile to see how stats have updated!");
        }
    }

    public Skill GetSkillByType(SkillType type)
    {
        return playerSkills.GetSkill(type);
    }

    public int GetStudyRate()
    {
        return studyRate;
    }
}