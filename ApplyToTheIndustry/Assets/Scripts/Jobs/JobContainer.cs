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

    public static JobContainer Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Another instance of JobContainer already exists. Destroying this one.");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Update UI text and logo based on first job posting
        jobTMPCompany.text = currentPosting.company.compName;
        jobTMPPosition.text = currentPosting.positionName;
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
        //jobTMPDescription.text = currentPosting.jobDescription;
        companyTMPLogo.sprite = currentPosting.company.logo;
    }
}

