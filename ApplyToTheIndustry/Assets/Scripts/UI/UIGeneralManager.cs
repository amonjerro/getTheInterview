using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGeneralManager : MonoBehaviour
{
    public List<InterfaceGroup> interfaceGroups;
    public InterfaceGroup feedbackScreen;
    public InterfaceGroup timerPanel;
    private InterfaceGroup wasActive;

    private void Start()
    {
        ServiceLocator.Instance.GetService<TimeManager>().D_timeout += MoveToFeedbackScreen;
    }
    public void MoveToFeedbackScreen()
    {
        foreach(InterfaceGroup ig in interfaceGroups)
        {
            ig.gameObject.SetActive(false);
        }

        feedbackScreen.gameObject.SetActive(true);
    }

    public void MoveToMainScreen()
    {
        foreach (InterfaceGroup ig in interfaceGroups)
        {
            ig.gameObject.SetActive(false);
        }

        timerPanel.gameObject.SetActive(true);
        ServiceLocator.Instance.GetService<TimeManager>().ResetTimer();
    }
    
    public void MoveToPauseScreen()
    {
        foreach (InterfaceGroup ig in interfaceGroups)
        {
            if (ig.gameObject.activeInHierarchy)
            {
                wasActive = ig;
            }
            ig.gameObject.SetActive(false);
        }
    }

    public void MoveAwayFromPauseScreen()
    {
        wasActive.gameObject.SetActive(true);
    }

}
