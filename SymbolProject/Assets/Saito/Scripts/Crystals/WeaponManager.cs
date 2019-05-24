using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private int[] weaponList = new int[8];
    
    [SerializeField]
    private GameObject weaponBoxes;
    
    private GameObject[] weaponBox = new GameObject[8];
    
    private int weaponBoxesCount;
    
    private void Start()
    {
        for (int i = 0; i < weaponBoxes.transform.childCount; i++)
        {
            weaponBox[i] = weaponBoxes.transform.GetChild(i).gameObject;
        }
        weaponList = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
    } 
    
    public void HaveWeapon()
    {
        
        for (int i = 0; i < weaponBox.Length; i++)
        {
            WeaponInfo wInfo = weaponBox[i].GetComponent<WeaponInfo>();
            weaponList[(int)wInfo.weaponList] += 1;
        }
        for (int i = 0; i < 8; i++)
        {
            Debug.Log("weapon"+ i + "=" + weaponList[i]);
        }
    }
}
