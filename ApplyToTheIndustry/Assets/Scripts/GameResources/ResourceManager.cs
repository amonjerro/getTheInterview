using UnityEngine;

// Class in charge of managing the data for resources
// It sends messages to the TimeManager class as it has some additional time-related
// behavior that doesn't fit the scope of this class.
// Should be a child of the ServiceLocator
public class ResourceManager : MonoBehaviour
{
    // Game resources
    private Resource _timeAvailable;
    private Resource _moneyAvailable;

    // Configurable parameters that set the limit of the game;
    [SerializeField]
    public Resource maxMoney;
    [SerializeField]
    public Resource maxTime;
    public MoneyUI moneyUI;
    private TimeManager _timeManager;

    private void Awake()
    {
        _timeAvailable = new Resource(maxTime.value);
        _moneyAvailable = new Resource(maxMoney.value);
        _timeManager = ServiceLocator.Instance.GetService<TimeManager>();
        moneyUI.UpdateMoneyText(_moneyAvailable.value);
    }

    // Support function - if we ever want to bind the maximums players can reset to,
    // this would be the way to do it.
    public void ReduceMaximums(Resource time, Resource money)
    {
        maxTime -= time;
        _timeManager.SetMaxTime(maxTime.value);
        maxMoney -= money;
    }

    // Test function for UI and other processes that need to know whether
    // a cost can be met with the available resources
    public bool IsCostViable(ActionCost cost)
    {
        bool enoughTime = _timeAvailable.IsThereEnough(cost.time.value);
        bool enoughMoney = _moneyAvailable.IsThereEnough(cost.money.value);
        return enoughMoney && enoughTime;
    }

    // Handle expending some cost
    public void ManageCost(ActionCost cost)
    {
        _timeAvailable -= cost.time;
        _timeManager.ExpendTime(cost.time.value);
        _moneyAvailable -= cost.money;
        moneyUI.UpdateMoneyText(_moneyAvailable.value);
    }

    // Reset the status of resources
    public void ResetResources()
    {
        _timeAvailable = maxTime;
        _timeManager.ResetTimer();
        _moneyAvailable = maxMoney;
        moneyUI.UpdateMoneyText(_moneyAvailable.value);
    }
}