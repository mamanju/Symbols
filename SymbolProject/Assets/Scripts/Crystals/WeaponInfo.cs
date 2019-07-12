using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    [SerializeField]
    public enum WeaponList
    {
        empty = -1,
        sword,
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
