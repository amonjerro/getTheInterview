using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(RectTransform))]
public class MouseOverColorChange : MonoBehaviour
{
    public Color mouseOverColor;
    TextMeshProUGUI text;
    RectTransform rt;
    Color originalColor;
    private void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        rt = GetComponent<RectTransform>();
        originalColor = text.color;
    }

    public void TurnMouseOverColor()
    {
        text.color = mouseOverColor;
    }

    public void RevertColor()
    {
        text.color = originalColor;
    }
}
