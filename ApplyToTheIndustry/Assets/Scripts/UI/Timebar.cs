using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    public Image backgroundImage;
    public Image barImage;
    private float _width;
    private float originalXPosition;
    RectTransform rt;

    public void Setup()
    {
        _width = backgroundImage.GetComponent<RectTransform>().rect.width;
        rt = barImage.GetComponent<RectTransform>();
        originalXPosition = rt.anchoredPosition.x;
    }

    public void SetFullness(float proportion)
    {
        float inverseProportion = 1 - proportion;
        float stepSize = _width / 100f;
        float widthMovement = stepSize * (inverseProportion * 50);
        rt.localScale = new Vector3(proportion, 1, 1);
        rt.localPosition = new Vector3(originalXPosition - widthMovement , rt.localPosition.y, rt.localPosition.z);
    }
}
