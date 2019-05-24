using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerStatusList
{
    public int HP;
    public int ATK;
    public enum WeaponList
    {
        sword,
        spear,
        ax,
        shield,
    }

    public WeaponList nowWeaponList;
}

public class PlayerHPSample : MonoBehaviour
{
    [SerializeField]
    private PlayerStatusList pStatusList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
