using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;

public class NewsTicker : MonoBehaviour
{
    public List<NewsObject> news;
    public float tickerSpeed;
    public int characterWidth;
    public string messageSeparator;
    public GameObject tickerHolder;
    RectTransform childTransform;
    
    public GameObject textPrefab;
    private NewsObject _activeObject;
    private int currentNewsDay = 0;
    private Vector3 originalTickerHolderPosition;

    public void Awake()
    {

        LoadTicker();
        TimeManager tmManager = ServiceLocator.Instance.GetService<TimeManager>();
        tmManager.D_timeout += LoadTicker;
        childTransform = tickerHolder.GetComponent<RectTransform>();
        originalTickerHolderPosition = childTransform.localPosition;
    }

    public void Update()
    {
        float newX = childTransform.localPosition.x;
        newX = newX - (tickerSpeed * Time.deltaTime);
        childTransform.localPosition = new Vector3(newX, 0, 0);

        if (childTransform.localPosition.x < -6 * originalTickerHolderPosition.x)
        {
            childTransform.localPosition = originalTickerHolderPosition;
        }
    }

    private void ClearNewsItems()
    {
        foreach(Transform child in tickerHolder.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void LoadTicker()
    {
        // Destroy any news children in the ticker;
        ClearNewsItems();

        // Load the current active object;
        _activeObject = news[currentNewsDay];
        string fullHeadline = "";
        foreach (string headline in _activeObject.headlines)
        {
            fullHeadline += headline + " " + messageSeparator + " ";
        }
        int newWidth = characterWidth * fullHeadline.Length;
        GameObject textGameObject = Instantiate(textPrefab, tickerHolder.transform);
        TextMeshProUGUI textComponent = textGameObject.GetComponent<TextMeshProUGUI>();
        textGameObject.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, newWidth);
        textComponent.text = fullHeadline;
        currentNewsDay++;
    }

}
