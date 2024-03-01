using UnityEngine;
using UnityEngine.UI;

public enum PanelTypes
{
    JobPostings,
    Courses
}

public interface IPanelDataStrategy
{
    public void LoadData(int day);
    public void DisplayData(GridLayoutGroup layoutContainer, GameObject prefab, InterfaceGroup dependentUI);
}

public class JobPostingPanelData : IPanelDataStrategy
{
    JobPosting[] data;
    public void LoadData(int day)
    {
        // Get the job manager and all job postings from current day
        JobManager jobMngr = ServiceLocator.Instance.GetService<JobManager>();
        PostingsByDay currentDayJobs = jobMngr.GetByDay(day);
        data = currentDayJobs.posts;
    }

    public void DisplayData(GridLayoutGroup layoutContainer, GameObject prefab, InterfaceGroup dependentUI)
    {
        foreach (JobPosting job in data)
        {
            // Create the button
            GameObject jobButton = Object.Instantiate(prefab);

            // Set it to be a child of the grid layout
            jobButton.transform.SetParent(layoutContainer.transform);

            // Add the job data
            JobContainer jobCont = jobButton.GetComponent<JobContainer>();
            jobCont.currentPosting = job;
            jobCont.UpdateUI();

            // Set action UI
            Action action = jobButton.GetComponent<Action>();
            action.dependentUI = dependentUI;
        }
    }
}

public class CoursePanelData : IPanelDataStrategy{

    CourseObject[] data;
    public void LoadData(int day)
    {
        // Get the job manager and all job postings from current day
        PlayerSkillsManager psm = ServiceLocator.Instance.GetService<PlayerSkillsManager>();
        CourseGroupingByDay currentCourses = psm.GetCoursesByDay(day);
        data = currentCourses.courses;
    }

    public void DisplayData(GridLayoutGroup group, GameObject prefab, InterfaceGroup dependentUI)
    {
        foreach (CourseObject course in data)
        {
            // Create the button
            GameObject courseButton = Object.Instantiate(prefab);

            // Set it to be a child of the grid layout
            courseButton.transform.SetParent(group.transform);

            // Set action UI
            Action action = courseButton.GetComponent<Action>();
            action.dependentUI = dependentUI;
        }
    }
}

public static class ChoosePanelStrategyFactory
{
    public static IPanelDataStrategy MakeStrategy(PanelTypes type)
    {
        switch (type)
        {
            case PanelTypes.Courses:
                return new CoursePanelData();
            default:
                return new JobPostingPanelData();
        }
    }
}