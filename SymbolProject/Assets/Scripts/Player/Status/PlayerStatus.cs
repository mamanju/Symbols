using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //public enum Weapon {
    //    Sword = -1,
    //    Spear,
    //    Ax,
    //    Shield
    //}

    //public Weapon nowWeapon;

    // 武器のストック(Weaponと同じ配置)
    //private int[] weaponStock = { 1, 0, 0, 0 };

    private int hp = 20;
    private int max_hp = 20;
    private int attack = 1;

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
        return attack;
    }

    //public int[] WeaponStock
    //{
    //    get { return weaponStock; }
    //    set { weaponStock = value; }
    //}

    // Start is called before the first frame update
    void Start()
    {
        int swordPower = SwordInfo.attack + attack;
        int spearPower = SpearInfo.attack + attack;
        int axPower = AxInfo.attack + attack;
    }

    private void Update()
    {
        
    }
}