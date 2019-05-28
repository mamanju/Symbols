using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSynthesisCrystal : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponBoxes;
    private GameObject[] weaponBox;

    private int weaponBoxCount;

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
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            for (int i = 0; i < weaponBoxCount; i++)
            {
                if (weaponBox[i].GetComponent<WeaponInfo>().weaponList == WeaponInfo.WeaponList.sword)
                {
                    weaponBox[i].GetComponent<WeaponInfo>().weaponList = this.GetComponent<WeaponInfo>().weaponList;
                    break;
                }
            }
            this.GetComponent<WeaponInfo>().weaponList = WeaponInfo.WeaponList.sword;
        }
    }
}
