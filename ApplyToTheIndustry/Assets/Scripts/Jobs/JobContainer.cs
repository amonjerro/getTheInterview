using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public Image companyTMPLogo;

    // Storage for job posting
    public JobPosting currentPosting;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Method for updating UI
    public void UpdateUI()
    {
        // Update UI text and logo based on current job posting
        if(currentPosting != null)
        {
            jobTMPCompany.text = currentPosting.company.compName;
            jobTMPPosition.text = currentPosting.positionName;
        }

        // Update the job description
        if (jobTMPDescription != null)
        {
            jobTMPDescription.text = "";
            foreach (string j in currentPosting.jobSummaryLines)
            {
                jobTMPDescription.text += j + "\n";
            }
            jobTMPDescription.text += "\n";
            foreach (string j in currentPosting.jobReqLines)
            {
                jobTMPDescription.text += j + "\n";
            }
        }

        // Update the company logo
        if (companyTMPLogo != null)
            companyTMPLogo.sprite = currentPosting.company.logo;
    }
}

