using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour
{
    public ResumeComponent resume;
    public ResumeComponent skillPanel;
    
    public void OnClick()
    {
        UIGeneralManager ugm = ServiceLocator.Instance.GetService<UIGeneralManager>();
        ugm.UpdatePopUp("Your application has been cancelled!");
        ugm.ShowPopUp();


         // Reset resume
        resume.Reset();
        skillPanel.Reset();
        
    }
}
