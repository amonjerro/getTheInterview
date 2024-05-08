using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FeedbackData
{
    public Dictionary<string, string> companyResponses;
    public Dictionary<string, bool> validApplications;
    public Dictionary<string, string> positionByCompany;
    public Dictionary<string, string> connectionFeedback;
    public Dictionary<string, bool> acceptedTo;

    public FeedbackData(Dictionary<string, string> companies, Dictionary<string, bool> application, Dictionary<string, string> positions, Dictionary<string, string> connection, Dictionary<string, bool> accepted)
    {
        companyResponses = companies;
        validApplications = application;
        positionByCompany = positions;
        connectionFeedback = connection;
        acceptedTo = accepted;
    }
}

public class Grader : MonoBehaviour
{
    Dictionary<SkillType, int> skillPoints = new Dictionary<SkillType, int>();
    List<SkillType> prioritization = new List<SkillType>();


    [Header("Possible Response Texts")]
    [Space(5)]
    public ApplicationResponseTexts veryPoorPerformanceResponses;
    public ApplicationResponseTexts betterPerformanceResponses;
    public ApplicationResponseTexts acceptancePerformanceResponses;
    public ApplicationResponseTexts competitionResponses;

    [HideInInspector]
    public List<string> feedback = new List<string>();
    [HideInInspector]
    public List<string> companiesAppliedTo = new List<string>();
    [HideInInspector]
    public List<string> positionsAppliedTo = new List<string>();
    private List<bool> positionsAcceptedTo = new List<bool>();
    int Totalpoints = 0;
    int gradingRequiredPoints = 0;
    [HideInInspector]
    public int companiesAppliedCount;
    Resume currentResume;
    JobPosting currentPosting;
    bool isGhosted;
    bool canGhost;
    float randomGhosting;

    [Header("Scoring Balance")]
    [Space(5)]
    public int prioritizationMismatchPenalty;
    public float ghostingRange;
    

    public List<string> companiesReceivedFeedback = new List<string>();
    public List<string> positionsReceivedFeedback = new List<string>();
    public Dictionary<string, string> connectionFeedback = new Dictionary<string, string>();
    public List<SkillType> companiesGradingFeedback = new List<SkillType>();
    private bool provideGradingFeedback;

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
        gradingRequiredPoints = posting.gradingProfile.skillGroup.TotalSum();
        companiesAppliedCount +=1;

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
            bool matchFound = false;
            for (int j = 0; j < prioritization.Count; j++)
            {
                // We have found a match
                if (prioritization[j] == posting.gradingProfile.expectedSkillOrdering[i])
                {
                    Totalpoints -= prioritizationMismatchPenalty * Mathf.Abs(i - j);
                    matchFound = true;
                    break;
                }
            }
            if (!matchFound)
            {
                // No match has been found
                Totalpoints -= prioritizationMismatchPenalty * 3;
            }
            
        }

        // Evaluate skill thresholds
        foreach(SkillType type in prioritization)
        {
            Totalpoints += EvaluateThreshold(type, posting);
        }


        ResourceManager rm = ServiceLocator.Instance.GetService<ResourceManager>();
        foreach(Connections connection in rm.connectionList)
        {
            if(connection.companyName == posting.company.name)
            {
                Totalpoints = Totalpoints + connection.connectionBonus;
            }
        }

        SetFeedback(posting);
    }

    public int EvaluateThreshold(SkillType type, JobPosting posting)
    {
        SkillGroup postSkillRequirements = posting.gradingProfile.skillGroup;
        switch (type)
        {
            case SkillType.Programming:
                if (skillPoints[type] >= postSkillRequirements.programming.value)
                {
                    return postSkillRequirements.programming.value;
                }
                else
                {
                    return 0;
                }
            case SkillType.Design:
                if (skillPoints[type] >= postSkillRequirements.design.value)
                {
                    return postSkillRequirements.design.value;
                }
                else
                {
                    return 0;
                }
            case SkillType.Graphics:
                if (skillPoints[type] >= postSkillRequirements.graphic_art.value)
                {
                    return postSkillRequirements.graphic_art.value;
                }
                else
                {
                    return 0;
                }
            case SkillType.Leadership:
                if (skillPoints[type] >= postSkillRequirements.leadership.value)
                {
                    return postSkillRequirements.leadership.value;
                }
                else
                {
                    return 0;
                }
            case SkillType.Sound:
                if (skillPoints[type] >= postSkillRequirements.sound_and_music.value)
                {
                    return postSkillRequirements.sound_and_music.value;
                }
                else
                {
                    return 0;
                }
            case SkillType.Production:
                if (skillPoints[type] >= postSkillRequirements.production.value)
                {
                    return postSkillRequirements.production.value;
                }
                else
                {
                    return 0;
                }
            case SkillType.ForeignLang:
                if (skillPoints[type] >= postSkillRequirements.foreign_lang.value)
                {
                    return postSkillRequirements.foreign_lang.value;
                }
                else
                {
                    return 0;
                }
            default:
                return 0;
        }
    }

    public void SetFeedback(JobPosting posting)
    {
        float chanceToGetGhosted = ghostingRange;
        string message = "";
        bool wasAccepted = false;
        if (Totalpoints < gradingRequiredPoints * 0.5f)
        {
            int index = Random.Range(0, veryPoorPerformanceResponses.responseCandidates.Length);
            message = veryPoorPerformanceResponses.responseCandidates[index];
            canGhost = true;
            provideGradingFeedback = true;

        }
        else if (Totalpoints >= gradingRequiredPoints * 0.5f && Totalpoints < gradingRequiredPoints * 0.75f)
        {
            int index = Random.Range(0, betterPerformanceResponses.responseCandidates.Length);
            message = betterPerformanceResponses.responseCandidates[index];
            canGhost = true;
            chanceToGetGhosted = ghostingRange * 0.5f;
            provideGradingFeedback = true;      
        } else
        {
            // We are above minimum hiring threshold.
            // So now we add competition into the mix
            float competitionRate = Random.Range(0.1f, 0.3f);
            float successLikelihood = Totalpoints / (float) gradingRequiredPoints;
            successLikelihood = Mathf.Clamp(successLikelihood - competitionRate, 0, 1);

            float dieRoll = Random.Range(0.0f, 1.0f);
            if (dieRoll < successLikelihood)
            {
                // You did it, you are big kahuna, congratz
                int index = Random.Range(0, acceptancePerformanceResponses.responseCandidates.Length);
                message = acceptancePerformanceResponses.responseCandidates[index];
                canGhost = false;
                chanceToGetGhosted = 0.0f;
                provideGradingFeedback = false;
                wasAccepted = true;
            } else
            {
                int index = Random.Range(0, competitionResponses.responseCandidates.Length);
                message = competitionResponses.responseCandidates[index];
                canGhost = true;
                chanceToGetGhosted = ghostingRange * 0.25f;
                provideGradingFeedback = true;
            }
        }
        positionsAcceptedTo.Add(wasAccepted);
        bool ghosted = toGhost(canGhost, chanceToGetGhosted);
        if(ghosted == false)
        {
            feedback.Add(message);
            companiesReceivedFeedback.Add(posting.company.name);
            positionsReceivedFeedback.Add(posting.positionName);
            companiesGradingFeedback.AddRange(posting.gradingProfile.expectedSkillOrdering);
        }

        AddConnectionFeedback();


        // Construct and save data
        ConstructAndSaveData(ghosted);
    }

    public void AddConnectionFeedback()
    {
        string connectionMessage = "";
        ResourceManager rm = ServiceLocator.Instance.GetService<ResourceManager>();
        foreach(Connections connection in rm.connectionList)
        {
            foreach(var i in companiesReceivedFeedback)
            {
                if(connection.companyName == i)
                {
                    if (provideGradingFeedback == true)
                    {
                        connectionMessage = $@"You are receiving this mail as you have a connection with one of the member of the company {connection.companyName}.

Message from your connection:

Unfortunately, your application was not successful this time. However, don't be discouraged! You can always try again, and we encourage you to enhance your skills in this order

1. {companiesGradingFeedback[0]}
2. {companiesGradingFeedback[1]}
3. {companiesGradingFeedback[2]}
4. {companiesGradingFeedback[3]}
5. {companiesGradingFeedback[4]}";
                    }
                    else
                    {
                        connectionMessage = $@"You are receiving this mail as you have a connection with one of the member of the company {connection.companyName}.
                                    Message from your connection:
                                    
                                    Congratulations on moving to the next round.";
                    }

                    connectionFeedback.Add(connection.companyName, connectionMessage);
                }
            }
        }
    }

    public FeedbackData GetFeedback()
    {
        Dictionary<string, bool> validApplications = new Dictionary<string, bool>();
        Dictionary<string, string> companyResponses = new Dictionary<string, string>();
        Dictionary<string, string> positionByCompany = new Dictionary<string, string>();
        Dictionary<string, bool> acceptedToPositions = new Dictionary<string, bool>();
        for (int i = 0; i < companiesAppliedTo.Count; i++)
        {
            bool receivedThisFeedback = companiesReceivedFeedback.Contains(companiesAppliedTo[i]);
            validApplications.Add(companiesAppliedTo[i], receivedThisFeedback);
            acceptedToPositions.Add(companiesAppliedTo[i], positionsAcceptedTo[i]);
            if (receivedThisFeedback)
            {
                int indexOf = companiesReceivedFeedback.IndexOf(companiesAppliedTo[i]);
                companyResponses.Add(companiesAppliedTo[i], feedback[indexOf]);
                positionByCompany.Add(companiesAppliedTo[i], positionsAppliedTo[i]);
            }
        }
        return new FeedbackData(companyResponses, validApplications, positionByCompany, connectionFeedback, acceptedToPositions);
    }

    public bool toGhost(bool ghosting, float range){
        if(ghosting == true)
        {
            randomGhosting = Mathf.Round(Random.Range(0.0f, 1.0f) * 100f) / 100f;
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

        return isGhosted;

    }

    public void ResetFeedback()
    {
        companiesAppliedTo.Clear();
        positionsAppliedTo.Clear();
        feedback.Clear();
        companiesGradingFeedback.Clear();
        connectionFeedback.Clear();
        positionsReceivedFeedback.Clear();
        companiesReceivedFeedback.Clear();
        positionsAcceptedTo.Clear();
    }

    public void ConstructAndSaveData(bool ghosted)
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
        resultsData.ghosted = ghosted;
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


