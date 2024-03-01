using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grader : MonoBehaviour
{
    Dictionary<SkillType, int> skillPoints = new Dictionary<SkillType, int>();
    List<SkillType> prioritization = new List<SkillType>();
    public List<string> feedback = new List<string>();
    public List<string> companiesAppliedTo = new List<string>();
    public List<string> positionsAppliedTo = new List<string>();
    int Totalpoints = 0;
    public int prioritizationMismatchPenalty;
    Resume currentResume;
    JobPosting currentPosting;
    bool isGhosted;
    bool canGhost;
    float randomGhosting;
    float ghostingRange;
    public int companiesAppliedCount;


    public void OnSubmit(Resume resume, JobPosting posting)
    {
        currentResume = resume;
        currentPosting = posting;

        positionsAppliedTo.Add(posting.positionName);
        companiesAppliedTo.Add(posting.company.name);
        // Clear all previous calculations and data structures
        skillPoints.Clear();
        prioritization.Clear();
        Totalpoints = 0;
        companiesAppliedCount +=1;

        // Access the selected skill list from the Resume instance
        List<Skill> selectedSkills = resume.selectedSkillsList;
        foreach (Skill skill in selectedSkills)
        {
            skillPoints[skill.skillType] = skill.value;
            prioritization.Add(skill.skillType);
        }
        Debug.Log(companiesAppliedCount);
        CalculateTotalPoints(posting);
    }

    public void CalculateTotalPoints(JobPosting posting)
    {
        // Evaluate priorities
        for (int i = 0; i < posting.gradingProfile.expectedSkillOrdering.Length; i++)
        {
            for (int j = 0; j < prioritization.Count; j++)
            {
                // We have found a match
                if (prioritization[j] == posting.gradingProfile.expectedSkillOrdering[i])
                {
                    Totalpoints -= prioritizationMismatchPenalty * Mathf.Abs(i - j);
                    break;
                }
            }
            // No match has been found
            Totalpoints -= 50;
        }

        // Evaluate skill thresholds
        foreach(SkillType type in prioritization)
        {
            Totalpoints += EvaluateThreshold(type, posting);
        }
        SetFeedback();
    }

    public int EvaluateThreshold(SkillType type, JobPosting posting)
    {
        SkillGroup postSkillRequirements = posting.gradingProfile.skillGroup;
        switch (type)
        {
            case SkillType.Programming:
                if (skillPoints[type] >= postSkillRequirements.programming.value)
                {
                    return skillPoints[type];
                }
                else
                {
                    return 0;
                }
            case SkillType.Design:
                if (skillPoints[type] >= postSkillRequirements.design.value)
                {
                    return skillPoints[type];
                }
                else
                {
                    return 0;
                }
            case SkillType.Graphics:
                if (skillPoints[type] >= postSkillRequirements.graphic_art.value)
                {
                    return skillPoints[type];
                }
                else
                {
                    return 0;
                }
            case SkillType.Leadership:
                if (skillPoints[type] >= postSkillRequirements.leadership.value)
                {
                    return skillPoints[type];
                }
                else
                {
                    return 0;
                }
            case SkillType.Sound:
                if (skillPoints[type] >= postSkillRequirements.sound_and_music.value)
                {
                    return skillPoints[type];
                }
                else
                {
                    return 0;
                }
            case SkillType.Production:
                if (skillPoints[type] >= postSkillRequirements.production.value)
                {
                    return skillPoints[type];
                }
                else
                {
                    return 0;
                }
            default:
                return 0;
        }
    }

    public void SetFeedback()
    {
        string message = "";
        if (Totalpoints < 15)
        {
            message = "THIS IS AN AUTOMATED RESPONSE. YOUR RESUME FAILED TO MEET MINIMUM EXPECTATIONS AND HAS BEEN REJECTED AUTOMATICALLY BY OUR AI REVIEWER.";
            canGhost = true;
            ghostingRange = 0.6f;
            Debug.Log("Range = "+ ghostingRange);

        }
        else if (Totalpoints >= 15 && Totalpoints <= 25)
        {
            message = "We have reviewed your application and regret to inform you that you have not been selected for the position. We wish you the best of luck in your professional career.";
            canGhost = true;
            ghostingRange = 0.4f;
            Debug.Log("Range = "+ ghostingRange);        
        }
        else if (Totalpoints >= 25 && Totalpoints <= 35)
        {
            message = "Your application has been reviwed by our Human Resources team but due to the competitive nature of this position, we are unable to proceed in this process with you. Thank you for considering applying to our company.";
            canGhost = true;
            ghostingRange = 0.2f;
            Debug.Log("Range = "+ ghostingRange);
        }
        else
        {
            message = "Let's discuss the next steps";
            canGhost = false;
            ghostingRange = 0.0f;
            Debug.Log("Range = "+ ghostingRange);

        }

        Debug.Log("Total points: "+Totalpoints);
        Debug.Log("Can Ghost="+canGhost);
        bool ghosted = toGhost(canGhost, ghostingRange);
        if(ghosted == false)
        {
            feedback.Add(message);
        }

        // Construct and save data
        ConstructAndSaveData();
    }

    public bool toGhost(bool ghosting, float range){
        if(ghosting == true)
        {
            randomGhosting = Mathf.Round(Random.Range(0.0f, 1.0f) * 100f) / 100f;
            Debug.Log("Randomness:"+randomGhosting);
            if(randomGhosting <= range)
            {
                isGhosted = true;
            }
            else
            {
                isGhosted = false;
            }
        }
        else
        {
            isGhosted = false;
        }

        
        Debug.Log("Ghosted: "+isGhosted);
        return isGhosted;

    }

    public void ResetFeedback()
    {
        companiesAppliedTo.Clear();
        positionsAppliedTo.Clear();
        feedback.Clear();
    }

    public void ConstructAndSaveData()
    {
        // Get job posting data
        JobData jobData = new JobData();
        jobData.companyName = companiesAppliedTo[companiesAppliedTo.Count - 1];
        jobData.positionName = positionsAppliedTo[positionsAppliedTo.Count - 1];

        // Get Resume data
        ResumeData resumeData = new ResumeData();
        List<SkillType> resumeOrder = new List<SkillType>();
        foreach (Skill skill in currentResume.selectedSkillsList)
        {
            resumeOrder.Add(skill.skillType);
        }
        resumeData.selectedOrder = resumeOrder.ToArray();

        // Get rubric data
        RubricData rubricData = new RubricData();
        rubricData.expectedOrder = currentPosting.gradingProfile.expectedSkillOrdering;

        // Get results data
        ResultsData resultsData = new ResultsData();
        resultsData.totalPoints = Totalpoints;
        //resultsData.ghosted = true;
        if(feedback.Count>1)
        {
            resultsData.returnMessage = feedback[feedback.Count - 1];
        }

        // Save the data
        SaveData dataToSave = new SaveData();
        dataToSave.jobData = jobData;
        dataToSave.resumeData = resumeData;
        dataToSave.rubricData = rubricData;
        dataToSave.resultsData = resultsData;
        ServiceLocator.Instance.GetService<DataSaver>().SaveData(dataToSave);
    }
}


