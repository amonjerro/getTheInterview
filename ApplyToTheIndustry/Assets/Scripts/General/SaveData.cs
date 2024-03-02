using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* CLASS: SaveData
 * Serializable class meant for storing
 * all structs made for saveable data
 */
[Serializable]
public class SaveData
{
    public JobData jobData;
    public ResumeData resumeData;
    public RubricData rubricData;
    public ResultsData resultsData;
}

// Public struct for holding
// saveable job data
[Serializable]
public struct JobData
{
    public string companyName;
    public string positionName;
}

// Public struct for holding
// saveable rubric data
[Serializable]
public struct RubricData
{
    public SkillType[] expectedOrder;
}

// Public struct for holding
// saveable resume data
[Serializable]
public struct ResumeData
{
    public SkillType[] selectedOrder;
}

// Public struct for holding
// saveable results data
[Serializable]
public struct ResultsData
{
    public int totalPoints;
    public bool ghosted;
    public string returnMessage;
}
