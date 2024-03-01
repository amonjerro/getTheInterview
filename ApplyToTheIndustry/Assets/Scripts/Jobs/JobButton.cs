using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class JobButton : MonoBehaviour
{
    // Public fields
    public JobContainer container;

    /// <summary>
    /// Event call for setting job manager's current job posting based
    /// on posting attached to this button's container
    /// </summary>
    public void OnClick()
    {
        ServiceLocator.Instance.GetService<JobManager>().SetCurrentPosting(container.currentPosting);
    }
}
