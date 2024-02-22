using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/* CLASS: JobContainer
 * Used for holding information on any individual job
 * posting that may be presented to the player
 */
public class JobContainer : MonoBehaviour
{
    // Fields to change
    public TextMeshProUGUI jobTMPCompany;
    public TextMeshProUGUI jobTMPPosition;
    public TextMeshProUGUI jobTMPDescription;

    // Storage for job posting
    public JobPosting currentPosting;

    // Start is called before the first frame update
    void Start()
    {
        // Update UI text based on first job posting
        jobTMPCompany.text = currentPosting.companyName;
        jobTMPPosition.text = currentPosting.positionName;
        jobTMPDescription.text = currentPosting.jobDescription;
    }
}
