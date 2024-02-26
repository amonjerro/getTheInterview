using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grader : MonoBehaviour
{
     Dictionary<string, int> skillPoints = new Dictionary<string, int>();
     HashSet<string> countedSkills = new HashSet<string>();
     int Totalpoints = 0;

    void Start()
    {
        
        GetListOfSkills();

    }

    void Update()
    {
        // Checking the job type
        if (ServiceLocator.Instance.GetService<JobManager>().builderPosting.currentPosting.name == "Job Posting for Programmer")
        {
            PointSystemForJobType1();

        }

        CalculateTotalPoints();

        GetFeedback();

    }

    //Get the list of skills available
    public void GetListOfSkills()
    {
        SkillGroup playerSkills = ServiceLocator.Instance.GetService<PlayerSkillsManager>().GetSkills();

        foreach (Skill skill in playerSkills.ListSkills())
        {
            if (!skillPoints.ContainsKey(skill.name))
            {
                skillPoints.Add(skill.name, 0); // Default points are 0
            }
        }
    }

    //Point system for job type 1
    public void PointSystemForJobType1()
    {
        skillPoints["Programming"] = 15;
        skillPoints["Design"] = 0;
        skillPoints["Aesthetics & Art"] = 0;
        skillPoints["Production"] = 5;
        skillPoints["Sound & Music"] = 10;
        skillPoints["Leadership"] = 10;
    }

    public void CalculateTotalPoints()
    {
        if (Resume.Instance != null)
        {
            // Access the selected skill list from the Resume instance
            List<Skill> selectedSkills = Resume.Instance.selectedSkillsList;
            
            foreach (Skill skill in selectedSkills)
            {
                if (!countedSkills.Contains(skill.name))
                {
                    if (skillPoints.ContainsKey(skill.name))
                    {
                        Totalpoints += skillPoints[skill.name];
                        countedSkills.Add(skill.name); // Add skill name to the set of counted skills
                    }
                    else
                    {
                        Debug.LogError("Skill not found in skillPoints dictionary: " + skill.name);
                    }
                }
            }
        Debug.Log("Total Points: " + Totalpoints);
        }
    }

    public void GetFeedback()
    {
        if (Totalpoints < 15)
        {
            Debug.Log("Better luck next time");
        }
        else if (Totalpoints >= 15 && Totalpoints <= 25)
        {
            Debug.Log("Please Improve your Skills");
        }
        else if (Totalpoints >= 25 && Totalpoints <= 35)
        {
            Debug.Log("Please learn new skills as well");
        }
        else
        {
            Debug.Log("Lets discuss the next steps");
        }
    }

}

