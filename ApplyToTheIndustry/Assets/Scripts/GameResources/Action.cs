using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    ResourceManager rm;
    public InterfaceGroup dependentUI;
    public ActionCost cost;

    // Start is called before the first frame update
    void Start()
    {
        rm = ServiceLocator.Instance.GetService<ResourceManager>();
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
    }
}
