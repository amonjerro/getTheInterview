using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public int money;
    public TextMeshProUGUI moneyTMP;

    // Start is called before the first frame update
    void Start()
    {
        moneyTMP.text = money.ToString();
    }

    public void ExpendMoney(int moneySpent)
    {
        money -= moneySpent;

        if(money <= 0)
        {
            money = 0;
            Debug.Log("No money left!");
        }

        moneyTMP.text = money.ToString();
    }
}
