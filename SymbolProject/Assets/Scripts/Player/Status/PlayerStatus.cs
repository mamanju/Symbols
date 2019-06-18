using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    private Sprite[] HpSprites;

    public enum Weapon {
        Sword,
        Spear,
        Ax,
        Shield
    }



    public Weapon nowWeapon;

    // 武器のストック(Weaponと同じ配置)
    private int[] weaponStock = { 1,0,0,0 };

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

    public int[] WeaponStock
    {
        get { return weaponStock; }
        set { weaponStock = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        int swordPower = SwordInfo.attack + attack;
        Debug.Log("剣の攻撃力:"+ swordPower);

        int spearPower = SpearInfo.attack + attack;
        Debug.Log("槍の攻撃力:" + spearPower);

        int axPower = AxInfo.attack + attack;
        Debug.Log("斧の攻撃力:" + axPower);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
