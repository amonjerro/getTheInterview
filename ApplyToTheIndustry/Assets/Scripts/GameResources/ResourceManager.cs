using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    private Resource _timeAvailable;
    private Resource _moneyAvailable;

    [SerializeField]
    public Resource maxMoney;
    [SerializeField]
    public Resource maxTime;

    public void ReduceMaximums(Resource time, Resource money)
    {
        maxTime -= time;
        maxMoney -= money;
    }

    public bool IsCostViable(Cost cost)
    {
        bool enoughTime = _timeAvailable.IsThereEnough(cost.time.value);
        bool enoughMoney = _moneyAvailable.IsThereEnough(cost.money.value);
        return enoughMoney && enoughTime;
    }

    public void ManageCost(Cost cost)
        {
        _timeAvailable -= cost.time;
        _moneyAvailable -= cost.money;
    }

    public void ResetResources()
    {
        _timeAvailable = maxTime;
        _moneyAvailable = maxMoney;
    }
}