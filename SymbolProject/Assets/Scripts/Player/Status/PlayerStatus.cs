using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private int hp = 20;
    private int max_hp = 20;
    private int attack = 1;

    private int nowAttack;
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
    }

    public void WeaponAttack(int _attack)
    {
        nowAttack = weaponAttacks[_attack];
    }
}