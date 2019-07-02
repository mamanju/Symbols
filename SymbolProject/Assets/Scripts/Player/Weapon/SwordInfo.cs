using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInfo : WeaponCtrl
{
    private GameObject player;
    private PlayerStatus playerStatus;

    void Awake()
    {
        player = this.transform.parent.parent.gameObject;
        Debug.Log(player.name);
        playerStatus = player.GetComponent<PlayerStatus>();

        // 剣の基本情報
        attack = 1;
        durable = -1;
        weaponID = "Sword";
    }

    void OnEnable()
    {
        playerStatus.WeaponAttack(attack);
    }
}
