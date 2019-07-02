using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    private GameObject nowWeapon;
    [SerializeField]
    private GameObject[] weapons = new GameObject[10];

    // Start is called before the first frame update
    void Start()
    {
        WeaponObjChange(0);
    }

    public void WeaponObjChange(int _weaponNum)
    {
        Destroy(nowWeapon);
        nowWeapon = Instantiate(weapons[_weaponNum]);
        nowWeapon.transform.parent = this.transform;
        nowWeapon.transform.localPosition = Vector3.zero;
    }
}
