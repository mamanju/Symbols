using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSynthesisCrystal : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponBoxes;
    private GameObject[] weaponBox;

    [SerializeField]
    private GameObject player;

    private int weaponBoxCount;
    private bool weaponMove;
    public bool WeaponMove
    {
        get { return weaponMove; }
        set { weaponMove = value; }
    }

    private bool endWeaponMove;

    void Start()
    {
        weaponBoxCount = weaponBoxes.transform.childCount;
        weaponBox = new GameObject[weaponBoxCount];
        for (int i = 0; i < weaponBoxCount; i++)
        {
            weaponBox[i] = weaponBoxes.transform.GetChild(i).gameObject;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<WeaponInfo>().weaponList == WeaponInfo.WeaponList.sword)  { return; }
        Debug.Log(weaponMove);
        if (weaponMove == true)
        {
            for (int i = 0; i < weaponBoxCount; i++)
            {
                if ((int)this.GetComponent<WeaponInfo>().weaponList == i && endWeaponMove == false)
                {
                    player.GetComponent<WeaponManager>().NowWeapon[i]++;
                    endWeaponMove = true;
                    weaponBox[i].GetComponent<WeaponInfo>().weaponList
                        = ((WeaponInfo.WeaponList)Enum.ToObject(typeof(WeaponInfo.WeaponList), i));
                    Debug.Log(player.GetComponent<WeaponManager>().NowWeapon[i]);
                    Debug.Log(endWeaponMove);
                    break;
                }
            }
            if (endWeaponMove == true)
            {
                this.GetComponent<WeaponInfo>().weaponList = WeaponInfo.WeaponList.sword;
                weaponMove = false;
                endWeaponMove = false;
            }
        }
    }
}
