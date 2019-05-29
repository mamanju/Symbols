using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInfo : WeaponCtrl
{
    public static int attack;
    void Start()
    {
        // 剣の基本情報
        attack = 1;
        durable = -1;
        weaponID = "Sword";
    }
}
