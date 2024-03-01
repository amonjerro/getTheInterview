using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfirmationPopup : MonoBehaviour
{
    public TextMeshProUGUI popupTMPText;

    /// <summary>
    /// Set new text for confirmation popup
    /// </summary>
    /// <param name="text">Text to set UI to</param>
    public void SetConfirmationText(string text)
    {
        popupTMPText.text = text;
    }
}
