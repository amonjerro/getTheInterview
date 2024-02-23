using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    public TimeManager tm;
    public MoneyManager mm;

    public int timeCost;
    public int moneyCost;

    // Start is called before the first frame update
    void Start()
    {
        tm = FindObjectOfType<TimeManager>();
        mm = FindObjectOfType<MoneyManager>();
    }

    public void DoAction()
    {
        if(tm.timeLeft >= timeCost && mm.money >= moneyCost)
        {
            tm.ExpendTime(timeCost);
            mm.ExpendMoney(moneyCost);

        }
    }
}
