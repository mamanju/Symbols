using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldInfo : WeaponCtrl
{
    public static int attack;
    public static int weaponID;

    void Start()
    {
        // 盾の基本情報
        attack = 0;
        durable = 5;
        durable_max = 5;
        weaponID = 3;
    }

    public void DelWeaponDurable()
    {
        Debug.Log("durable=" + durable);
        if (durable <= 0) { return; }
        base.BreakWeaponCheck(1);
        if (durable == 0)
        {
            GameObject player = this.transform.parent.parent.gameObject;
            player.GetComponent<PlayerCtrl>().WeaponChangeLeft();
            player.GetComponent<WeaponManager>().DeleteWeapon(weaponID - 1);
            durable = durable_max;
        }
    }

    public void Defense()
    {

    }
    
}
