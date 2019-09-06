using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInfo : WeaponController
{
    public static int attack;
    public static int weaponID;

    private GameObject player;

    void Start()
    {
        // 剣の基本情報
        attack = 1;
        durable = -1;
        durable_max = -1;
        weaponID = 0;

        player = transform.parent.GetComponent<GetPlayer>().Player;
    }



    private void OnTriggerEnter(Collider other)
    {
       player.GetComponent<PlayerController>().Attack(other.gameObject);
    }
}
