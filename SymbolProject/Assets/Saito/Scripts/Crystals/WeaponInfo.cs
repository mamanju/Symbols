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
        meteo,
        question,
        exclamation,
    };

    public WeaponList weaponList;
}
