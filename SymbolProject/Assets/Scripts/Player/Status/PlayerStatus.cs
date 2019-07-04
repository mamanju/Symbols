﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private int hp = 20;
    private int max_hp = 20;
    private int attack = 1;
    private KnockBack knockBack;

    private int nowAttack;
    private int nowWeaponID;
    public int NowWeaponID
    {
        get { return nowWeaponID; }
    }
    private int[] weaponAttacks = new int[10];
    
    public int PlayerHp
    {
        get { return hp; }
        set { hp = value; }
    }

    public int PlayerMax_Hp()
    {
        return max_hp;
    }

    public int PlayerAttack()
    {
        return nowAttack;
    }

    private void Start()
    {
        weaponAttacks[0] = SwordInfo.attack + attack;
        weaponAttacks[1] = SpearInfo.attack + attack;
        weaponAttacks[2] = AxInfo.attack + attack;
        weaponAttacks[3] = ShieldInfo.attack + attack;
        weaponAttacks[6] = CymbalsInfo.attack + attack;

        //完成する当たり使用するかも
        //for (int i = 0; i < weaponAttacks.Length; i++)
        //{
        //
        //}
        nowAttack = weaponAttacks[0];
    }

    private void Update()
    {
        if (hp != 0) { return; }
        
        //ゲームオーバーの処理
    }

    public void WeaponAttack(int _attack)
    {
        nowAttack = weaponAttacks[_attack];
        nowWeaponID = _attack;
    }


    //HP減少
    public void DownHP(int _damage)
    {
        hp -= _damage;
        knockBack = GetComponent<KnockBack>();
        knockBack.Knockback();
    }
}