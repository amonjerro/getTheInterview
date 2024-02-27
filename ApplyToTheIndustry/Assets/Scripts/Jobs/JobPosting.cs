using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* CLASS: JobPosting
 * Scriptable Object used mainly for storing
 * all information on any individual job posting
 */
[CreateAssetMenu(menuName = "ScriptableObjects/JobPosting")]
public class JobPosting : ScriptableObject
{
    // Data that the player sees
    public Company company;
    public string positionName;
    public string[] jobSummaryLines;
    public string[] jobReqLines;
    public GradingProfile gradingProfile;

    // TO-DO
    // Add other data here that'll be used for tracking 
    // backend of job requirements
}
