using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoneyData
{
    private static int money = 0;

    public static int Money
    {
        get => money;
        set => money = value;
    }
}
