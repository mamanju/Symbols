using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInfo : WeaponCtrl
{
    public static int attack;
    public static int weaponID;

    void Start()
    {
        // 剣の基本情報
        attack = 1;
        durable = -1;
        durable_max = -1;
        weaponID = 0;
    }
}
