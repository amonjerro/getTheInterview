using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMenu : MonoBehaviour
{
    /// <summary>
    /// Hides game object upon being called through
    /// OnClick event
    /// </summary>
    public void DisableObject()
    {
        Debug.Log("closing window");
        transform.parent.parent.gameObject.SetActive(false);
        ServiceLocator.Instance.GetService<UIGeneralManager>().UpdateErrorStatus(false);
    }
}
