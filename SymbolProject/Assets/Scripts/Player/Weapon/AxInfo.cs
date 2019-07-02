using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxInfo : WeaponCtrl
{
    private GameObject player;
    private PlayerStatus playerStatus;

    void Start()
    {
        // 斧の基本情報
        attack = 3;
        durable = 5;
        weaponID = "Ax";

        player = this.transform.parent.parent.gameObject;
        playerStatus = player.GetComponent<PlayerStatus>();
    }

    void OnEnable()
    {
        playerStatus.WeaponAttack(attack);    
    }
}
