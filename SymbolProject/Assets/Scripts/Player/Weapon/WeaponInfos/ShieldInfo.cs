using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldInfo : WeaponController
{
    public static int attack;
    public static int weaponID;

    [SerializeField]
    private GameObject shield;

    private GameObject player;

    void Start()
    {
        // 盾の基本情報
        attack = 0;
        durable = 5;
        durable_max = 5;
        weaponID = 3;
        player = transform.parent.GetComponent<GetPlayer>().Player;
    }

    private void OnEnable()
    {
        shield.SetActive(true);
    }

    private void OnDisable()
    {
        shield.SetActive(false);
    }

    public void DelWeaponDurable()
    {
        Debug.Log("durable=" + durable);
        if (durable <= 0) { return; }
        base.BreakWeaponCheck(1);
        if (durable == 0)
        {
            player.GetComponent<PlayerCtrl>().WeaponChangeLeft();
            player.GetComponent<WeaponManager>().DeleteWeapon(weaponID - 1);
            durable = durable_max;
        }
    }

    public void Defense()
    {

    }
    
}
