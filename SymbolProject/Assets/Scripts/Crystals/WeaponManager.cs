using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private static int[] nowWeapon = new int[] { 1, 1, 1, 0, 3, 0, 0, 0, 0 };
    public static int[] NowWeapon
    {
        get { return nowWeapon; }
        set { nowWeapon = value; }
    }
    
    [SerializeField]
    private GameObject weaponBoxes;
    
    private GameObject[] weaponBox = new GameObject[9];
    
    private int weaponBoxesCount;
    
    private void Start()
    {
        for (int i = 0; i < weaponBoxes.transform.childCount; i++)
        {
            weaponBox[i] = weaponBoxes.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        for (int i = 0; i < nowWeapon.Length; i++)
        {
            if (nowWeapon[i] == 0)
            {
                weaponBox[i].GetComponent<WeaponInfo>().weaponList = WeaponInfo.WeaponList.empty;
            }
        }
    }

    public void HaveWeapon()
    {
        for (int i = 0; i < weaponBox.Length; i++)
        {
            if (nowWeapon[i] >= 0)
            {
                weaponBox[i].GetComponent<WeaponInfo>().weaponList
                    = ((WeaponInfo.WeaponList)Enum.ToObject(typeof(WeaponInfo.WeaponList), i + 1));
            }
            else
            {
                weaponBox[i].GetComponent<WeaponInfo>().weaponList
                    = WeaponInfo.WeaponList.empty;
            }
        }
    }

    public void DeleteWeapon(int _num)
    {
        if(nowWeapon[_num] == 0) { return; }
        nowWeapon[_num]--;
        Debug.Log("ｗｗｗｗ" + nowWeapon[_num]);
    }
}
