using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_collider : MonoBehaviour
{
    [SerializeField]
    private GameObject wrist;

    private GameObject[] weapons = new GameObject[10];

    private BoxCollider[] boxColliders = new BoxCollider[10];

    private bool collider_Flag = false;

    private float colliderTime;

    // 槍のコライダーのディレイ
    private float spearTime;

    // 斧のコライダーのディレイ
    private float axeTime;

    [SerializeField]
    private float max_colliderTime = 0.5f;

    [SerializeField]
    private float max_spearTime = 0.3f;

    [SerializeField]
    private float max_axeTime = 0.3f;


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
        spearTime = max_spearTime;
        axeTime = max_axeTime;

    }

    private void Update()
    {
        if (collider_Flag == false) { return; }


        // if文の中身ちゃんと書き換える！！！！！
        // 持っている武器が槍だったら
        if (weapons[1])
        {
            spearTime -= Time.deltaTime;
            if (spearTime <= 0)
            {
                colliderTime -= Time.deltaTime;
                if (colliderTime <= 0)
                {
                    OffCollider(num);
                    collider_Flag = false;
                    colliderTime = max_colliderTime;
                }
            }
            return;
        }

        // 持っている武器が斧だったら
        if (weapons[2])
        {
            axeTime -= Time.deltaTime;
            if (axeTime <= 0)
            {
                colliderTime -= Time.deltaTime;
                if (colliderTime <= 0)
                {
                    OffCollider(num);
                    collider_Flag = false;
                    colliderTime = max_colliderTime;
                }
            }
            return;
        }

        colliderTime -= Time.deltaTime;
        if (colliderTime <= 0)
        {
            OffCollider(num);
            collider_Flag = false;
            colliderTime = max_colliderTime;
        }
    }


    public void OnCollider(int _num)
    {
        num = _num;
        if (_num >= 3) { return; }
        boxColliders[_num].enabled = true;
        collider_Flag = true;
    }

    public void OffCollider(int _num)
    {
        if (_num >= 3) { return; }
        boxColliders[_num].enabled = false;
    }
}
