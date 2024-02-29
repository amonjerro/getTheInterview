using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Action : MonoBehaviour
{
    ResourceManager rm;
    public InterfaceGroup dependentUI;
    public ActionCost cost;
    Grader feedback;

    // Start is called before the first frame update
    void Start()
    {
        rm = ServiceLocator.Instance.GetService<ResourceManager>();
        feedback = ServiceLocator.Instance.GetService<Grader>();
    }

    public void DoAction()
    {

        if(rm.IsCostViable(cost))
        {
            rm.ManageCost(cost);
        }
    }

    public void OpenInterface()
    {
        DoAction();
        dependentUI.gameObject.SetActive(true);
        gameObject.GetComponentInParent<InterfaceGroup>().gameObject.SetActive(false);

        // Get the popup panel and disable if changing interfaces
        HideMenu hiddenPanel = FindObjectOfType<HideMenu>();
        if(hiddenPanel != null)
        {
            // Get job manager to check if time was wasted before disabling
            JobManager jobMngr = ServiceLocator.Instance.GetService<JobManager>();
            if(!jobMngr.playerWastedTime)
                hiddenPanel.DisableObject();
        }
    }

}
