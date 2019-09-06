using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_collider : MonoBehaviour
{
    [SerializeField]
    private GameObject wrist;

    private GameObject[] weapons = new GameObject[10];

    private BoxCollider[] boxColliders = new BoxCollider[10];

    private SphereCollider sphereCollider;

    private int nowWeapon;

    private bool weaponCollider_Flag = false;

    public bool SetCollider_Flag
    {
        set { weaponCollider_Flag = value; }
    }

    private float colliderTime;

    // 剣のコライダーディレイ
    private float swordTime;

    // 槍のコライダーのディレイ
    private float spearTime;

    // 斧のコライダーのディレイ
    private float axeTime;

    // シンバルのコライダ―のディレイ
    private float cymbalsTime;

    private PlayerController pControl;

    [SerializeField]
    private float max_colliderTime = 0.5f;

    [SerializeField]
    private float max_swordTime = 0.5f;

    [SerializeField]
    private float max_spearTime = 0.3f;

    [SerializeField]
    private float max_axeTime = 0.3f;

    [SerializeField]
    private float max_cymbalsTime = 0.3f;

    private bool swordFlag = false;

    private bool spearFlag = false;

    private bool axeFlag = false;

    private bool cymbalsFlag = false;

    private bool colliderFlag = false;

    private bool spherecolliderFlag = false;

    private int num;

    void Start()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i] = wrist.transform.GetChild(i).gameObject;
            if (i == 5)
            {
                sphereCollider = weapons[i].GetComponent<SphereCollider>();
            }

            if (i <= 3)
            {
                boxColliders[i] = weapons[i].GetComponent<BoxCollider>();
            }

        }


        colliderTime = max_colliderTime;
        swordTime = max_swordTime;
        spearTime = max_spearTime;
        axeTime = max_axeTime;
        cymbalsTime = max_cymbalsTime;
        
    }

    private void Update()
    {
        if (colliderFlag)
        {
            ColliderTime();
        }

        if (spherecolliderFlag)
        {
            SphereColliderTime();
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

        if (cymbalsFlag)
        {
            cymbalsTime -= Time.deltaTime;
            if (cymbalsTime <= 0)
            {
                OnSphereCollider(nowWeapon);
                spherecolliderFlag = true;
                cymbalsFlag = false;
                cymbalsTime = max_cymbalsTime;
            }
        }

        if (weaponCollider_Flag == false) { return; }

        WeaponColliderTime();
    }


    public void OnCollider(int _num)
    {
        num = _num;
        if (_num >= 6) { return; }
        boxColliders[_num].enabled = true;
    }

    public void OnSphereCollider(int _num)
    {
        num = _num;
        if (_num >= 6) { return; }
        sphereCollider.enabled = true;
    }

    public void OffCollider(int _num)
    {
        if (_num >= 6) { return; }
        boxColliders[_num].enabled = false;
    }

    public void OffSphereCollider(int _num)
    {
        if (_num >= 6) { return; }
        sphereCollider.enabled = false;
    }

    public void ColliderTime()
    {
        colliderTime -= Time.deltaTime;
        if (colliderTime <= 0)
        {
            colliderFlag = false;
            OffCollider(num);
            colliderTime = max_colliderTime;
            PlayerController.instance.DownDurable();
            PlayerController.instance.AttackFlag = true;
        }
    }

    public void SphereColliderTime()
    {
        colliderTime -= Time.deltaTime;
        if (colliderTime <= 0)
        {
            spherecolliderFlag = false;
            OffSphereCollider(num);
            colliderTime = max_colliderTime;
            PlayerController.instance.DownDurable();
            PlayerController.instance.AttackFlag = true;
        }
    }

    private void WeaponColliderTime()
    {
        // 今持っている武器の番号をPlayerCtrlから取得
        nowWeapon = GetComponent<PlayerController>().GetWeaponNumber;

        if (nowWeapon == 0)
        {
            weaponCollider_Flag = false;
            swordFlag = true;
        }

        if (nowWeapon == 1)
        {
            weaponCollider_Flag = false;
            spearFlag = true;
        }

        if (nowWeapon == 2)
        {
            weaponCollider_Flag = false;
            axeFlag = true;
        }

        if (nowWeapon == 5)
        {
            weaponCollider_Flag = false;
            cymbalsFlag = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (nowWeapon != 5) { return; }

        GetComponent<PlayerController>().Attack(other.gameObject);
    }
}
