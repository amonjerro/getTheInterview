using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    public void UpdateMoneyText(int value)
    {
        moneyText.text = value.ToString();
    }
}
