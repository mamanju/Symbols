﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponTextManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private int[] nowWeapon = new int[9];

    private GameObject childText;

    // Start is called before the first frame update
    void Start()
    {
        childText = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        nowWeapon = player.GetComponent<WeaponManager>().NowWeapon;
        for (int i = 0; i < nowWeapon.Length; i++)
        {
            if (i == (int)this.GetComponent<WeaponInfo>().weaponList)
            {
                childText.GetComponent<Text>().text = "×" + nowWeapon[i].ToString();
            }
            else if (this.GetComponent<WeaponInfo>().weaponList == WeaponInfo.WeaponList.sword)
            {
                childText.GetComponent<Text>().text = "×0";
            }
        }
    }
}