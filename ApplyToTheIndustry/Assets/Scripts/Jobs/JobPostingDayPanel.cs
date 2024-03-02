
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePanel : MonoBehaviour
{
    // Public fields
    public GameObject buttonPrefab;
    public InterfaceGroup dependentUI;
    public PanelTypes panelType;
    private IPanelDataStrategy panelDataStrategy;

    /// <summary>
    /// Once enabled take all of the current day job postings
    /// and display them on the GUI for the player to select
    /// </summary>
    void OnEnable()
    {
        int currentWeek = ServiceLocator.Instance.GetService<TimeManager>().GetCurrentWeek();
        panelDataStrategy = ChoosePanelStrategyFactory.MakeStrategy(panelType);
        panelDataStrategy.LoadData(currentWeek);
        // Get the grid layout group element and add all job buttons to it
        GridLayoutGroup layout = GetComponentInChildren<GridLayoutGroup>();
        panelDataStrategy.DisplayData(layout, buttonPrefab, dependentUI);
        
    }

    /// <summary>
    /// Clean up the grid every time it gets disabled
    /// </summary>
    void OnDisable()
    {
        // Get the grid layout and clear all objects within the grid
        GridLayoutGroup layout = GetComponentInChildren<GridLayoutGroup>();
        for(int i = 0; i < layout.transform.childCount; i++)
        {
            Destroy(layout.transform.GetChild(i).gameObject);
        }
    }
}
