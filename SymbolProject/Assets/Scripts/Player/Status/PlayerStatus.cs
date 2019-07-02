using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private int hp = 20;
    private int max_hp = 20;
    private int attack = 1;

    [SerializeField]
    private WeaponCtrl weaponCtrl;
    private int nowAttack;
    
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

    public void WeaponAttack(int _attack)
    { 
        nowAttack = _attack + attack;
    }
}