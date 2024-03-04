using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Logger
{
    public static void Log(string message)
    {
        UIGeneralManager uigm = ServiceLocator.Instance.GetService<UIGeneralManager>();
        uigm.UpdatePopUp(message);
        uigm.ShowPopUp();
        uigm.UpdateErrorStatus(true);
    }
}
