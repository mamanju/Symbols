using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CymbalsInfo : WeaponCtrl
{
    public static int attack;
    public static int weaponID;

    [SerializeField]
    private GameObject leftCymbal;

    void Start()
    {
        // シンバルの基本情報
        attack = -1;
        durable = 5;
        durable_max = 5;
        weaponID = 6;
    }

    private void OnEnable()
    {
        leftCymbal.SetActive(true);
    }

    private void OnDisable()
    {
        leftCymbal.SetActive(false);
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
}
