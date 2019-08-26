using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_collider : MonoBehaviour
{
    [SerializeField]
    private GameObject wrist;

    private GameObject[] weapons = new GameObject[10];

    private BoxCollider[] boxColliders = new BoxCollider[10];

    private int nowWeapon;

    private bool collider_Flag = false;

    public bool SetCollider_Flag
    {
        set { collider_Flag = value; }
    }

    private float colliderTime;

    // 剣のコライダーディレイ
    private float swordTime;

    // 槍のコライダーのディレイ
    private float spearTime;

    // 斧のコライダーのディレイ
    private float axeTime;

    private PlayerCtrl pControl;

    [SerializeField]
    private float max_colliderTime = 0.5f;

    [SerializeField]
    private float max_swordTime = 0.5f;

    [SerializeField]
    private float max_spearTime = 0.3f;

    [SerializeField]
    private float max_axeTime = 0.3f;

    private bool swordFlag = false;

    private bool spearFlag = false;

    private bool axeFlag = false;

    private bool colliderFlag = false;


    private int num;

    void Start()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i >= 3) { return; }
            weapons[i] = wrist.transform.GetChild(i).gameObject;
            boxColliders[i] = weapons[i].GetComponent<BoxCollider>();
        }

        colliderTime = max_colliderTime;
        swordTime = max_swordTime;
        spearTime = max_spearTime;
        axeTime = max_axeTime;

        
    }

    private void Update()
    {
        if (colliderFlag)
        {
            ColliderTime();
        }

        if (swordFlag)
        {
            swordTime -= Time.deltaTime;
            if (swordTime <= 0)
            {
                OnCollider(nowWeapon);
                colliderFlag = true;
                swordFlag = false;
                swordTime = max_swordTime;
            }
        }

        if (spearFlag)
        {
            spearTime -= Time.deltaTime;
            if (spearTime <= 0)
            {
                OnCollider(nowWeapon);
                colliderFlag = true;
                spearFlag = false;
                spearTime = max_spearTime;
            }
        }

        if (axeFlag)
        {
            axeTime -= Time.deltaTime;
            if (axeTime <= 0)
            {
                OnCollider(nowWeapon);
                colliderFlag = true;
                axeFlag = false;
                axeTime = max_axeTime;
            }
        }

        if (collider_Flag == false) { return; }

        WeaponColliderTime();
    }


    public void OnCollider(int _num)
    {
        num = _num;
        if (_num >= 3) { return; }
        boxColliders[_num].enabled = true;
    }

    public void OffCollider(int _num)
    {
        if (_num >= 3) { return; }
        boxColliders[_num].enabled = false;
    }

    public void ColliderTime()
    {
        colliderTime -= Time.deltaTime;
        if (colliderTime <= 0)
        {
            OffCollider(num);
            colliderTime = max_colliderTime;
            colliderFlag = false;
        }
    }

    private void WeaponColliderTime()
    {
        // 今持っている武器の番号をPlayerCtrlから取得
        nowWeapon = GetComponent<PlayerCtrl>().GetWeaponNumber;

        if (nowWeapon == 0)
        {
            collider_Flag = false;
            swordFlag = true;
        }

        if (nowWeapon == 1)
        {
            collider_Flag = false;
            spearFlag = true;
        }

        if (nowWeapon == 2)
        {
            collider_Flag = false;
            axeFlag = true;
        }
    }
}
