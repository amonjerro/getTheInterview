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

    // Support function - if we ever want to bind the maximums players can reset to,
    // this would be the way to do it.
    public void ReduceMaximums(Resource time, Resource money)
    {
        maxTime -= time;
        ServiceLocator.Instance.GetService<TimeManager>().SetMaxTime(maxTime.value);
        maxMoney -= money;
    }

    // Test function for UI and other processes that need to know whether
    // a cost can be met with the available resources
    public bool IsCostViable(Cost cost)
    {
        bool enoughTime = _timeAvailable.IsThereEnough(cost.time.value);
        bool enoughMoney = _moneyAvailable.IsThereEnough(cost.money.value);
        return enoughMoney && enoughTime;
    }

    // Handle expending some cost
    public void ManageCost(Cost cost)
    {
        _timeAvailable -= cost.time;
        ServiceLocator.Instance.GetService<TimeManager>().ExpendTime(cost.time.value);
        _moneyAvailable -= cost.money;
    }

    // Reset the status of resources
    public void ResetResources()
    {
        _timeAvailable = maxTime;
        ServiceLocator.Instance.GetService<TimeManager>().ResetTimer();
        _moneyAvailable = maxMoney;
    }
}