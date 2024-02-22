using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    private Time _timeAvailable;
    private Money _moneyAvailable;

    [SerializeField]
    public Money maxMoney;
    [SerializeField]
    public Time maxTime;


    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else
        {
            Instance = this;
        }
    }

    void AffectMaximums(Time time, Money money)
    {
        maxTime -= time;
        maxMoney -= money;
    }
}