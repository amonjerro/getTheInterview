
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSkillsManager : MonoBehaviour
{
    public CourseGroupingByDay[] courseGroupings;
    [SerializeField]
    private int studyRate =  10;
    private bool courseIsBooked = false;
    public UIBar progressBar;
    public TextMeshProUGUI progressText;
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
        int index = day;
        if (day >= courseGroupings.Length)
        {
            index = 0;
        }
        return courseGroupings[index];
    }

    public void SetBookedCourse(CourseData data)
    {
        bookedCourse = data;
        courseIsBooked = true;
        progressBar.SetFullness(0);

        // Set text for total number of sessions in course
        float sessions = Mathf.Ceil(data.timeCost.value / (float)studyRate);
        progressText.text = "Sessions Left: " + sessions;


        ServiceLocator.Instance.GetService<UIGeneralManager>().SetProgressPanelStatus(courseIsBooked);
        ServiceLocator.Instance.GetService<UIGeneralManager>().UpdateCourseUI();
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

        // Update text with number of sessions left for course
        float sessions = Mathf.Ceil(((1 - bookedCourse.GetProgressPercent()) * bookedCourse.timeCost.value) / studyRate);
        progressText.text = "Sessions Left: " + sessions;

        if (bookedCourse.isComplete())
        {
            int bonus = bookedCourse.certification ? 2 : 1;
            AddToSkills(bookedCourse.reward * bonus);
            courseIsBooked = false;
            UIGeneralManager uigm = ServiceLocator.Instance.GetService<UIGeneralManager>();
            uigm.SetProgressPanelStatus(courseIsBooked);
            uigm.ShowPopUp();
            uigm.UpdatePopUp("You have finished studying your course! Check your profile to see how stats have updated!");
            ServiceLocator.Instance.GetService<UIGeneralManager>().UpdateCourseUI();
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