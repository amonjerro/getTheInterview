using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BouncyText : MonoBehaviour
{
    TextMeshProUGUI textToBounce;
    public float frequency;
    public float amplitude;
    private float originalFontSize;

    private void OnEnable()
    {
        textToBounce = GetComponent<TextMeshProUGUI>();
        originalFontSize = textToBounce.fontSize;
    }

    private float BounceEffectOutput(float x)
    {
        return 1+Mathf.Max(Mathf.Sin(frequency * x) * amplitude, 0);
    }

    private void Update()
    {
        textToBounce.fontSize = originalFontSize * BounceEffectOutput(Time.time);
    }
}
