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
    public int connectionBonusMin;
    public int connectionBonusMax;
    public int prioritizationMismatchPenalty;

    public void OnSubmit(Resume resume, JobPosting posting)
    {
        positionsAppliedTo.Add(posting.positionName);
        companiesAppliedTo.Add(posting.company.name);
        // Clear all previous calculations and data structures
        skillPoints.Clear();
        prioritization.Clear();
        Totalpoints = 0;

        // Access the selected skill list from the Resume instance
        List<Skill> selectedSkills = resume.selectedSkillsList;
        foreach (Skill skill in selectedSkills)
        {
            skillPoints[skill.skillType] = skill.value;
            prioritization.Add(skill.skillType);
        }
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

        Debug.Log(Totalpoints);

        ResourceManager rm = ServiceLocator.Instance.GetService<ResourceManager>();
        foreach(Connections connection in rm.connectionList)
        {
            if(connection.companyName == posting.company.name)
            {
                int chance = Random.Range(connectionBonusMin, connectionBonusMax);
                Totalpoints = Totalpoints + chance;
                Debug.Log(Totalpoints);
            }
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

        }
        else if (Totalpoints >= 15 && Totalpoints <= 25)
        {
            message = "We have reviewed your application and regret to inform you that you have not been selected for the position. We wish you the best of luck in your professional career.";
        }
        else if (Totalpoints >= 25 && Totalpoints <= 35)
        {
            message = "Your application has been reviwed by our Human Resources team but due to the competitive nature of this position, we are unable to proceed in this process with you. Thank you for considering applying to our company.";
        }
        else
        {
            message = "Let's discuss the next steps";
        }

        feedback.Add(message);
    }

    public void ResetFeedback()
    {
        companiesAppliedTo.Clear();
        positionsAppliedTo.Clear();
        feedback.Clear();
    }

}


