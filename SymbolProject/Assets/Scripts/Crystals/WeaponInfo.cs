using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    [SerializeField]
    public enum WeaponList
    {
        sword,
        spear,
        ax,
        shield,
        twinSword,
        cymbal,
        hammer,
        meteo,
    };

    public WeaponList weaponList;
}
