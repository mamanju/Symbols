using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxInfo : WeaponCtrl
{
    public static int attack;

    void Start()
    {
        // 斧の基本情報
        attack = 3;
        durable = 5;
        weaponID = "Ax";
    }
}
