using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    [SerializeField]
    public enum WeaponList
    {
        sword = -1,
        spear,
        ax,
        shield,
        twinSword,
        cymbal,
        hammer,
        meteor,
        question,
        exclamation,
    };

    public WeaponList weaponList;
}
