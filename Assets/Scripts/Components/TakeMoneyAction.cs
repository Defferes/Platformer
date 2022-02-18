using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeMoneyAction : MonoBehaviour
{
    [SerializeField] private int Nominal;
    public void TakeMoney()
    {
        MoneyData.Money += Nominal;
        Debug.Log("Монеты: " + MoneyData.Money);
        Destroy(gameObject);
    }
}
