using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    private GameObject nowWeapon;
    [SerializeField]
    private GameObject[] weapons = new GameObject[10];

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        weapons[0].SetActive(true);
        nowWeapon = weapons[0];
    }

    public void WeaponObjChange(int _weaponNum)
    {
        nowWeapon.SetActive(false);

        weapons[_weaponNum].SetActive(true);
        nowWeapon = weapons[_weaponNum];
    }

    public void WeaponDel(int _num)
    {
        switch(_num)
        {
            case 0:
                break;
            case 1:
                weapons[_num].GetComponent<SpearInfo>().DelWeaponDurable();
                break;
            case 2:
                weapons[_num].GetComponent<AxInfo>().DelWeaponDurable();
                break;
            case 3:
                weapons[_num].GetComponent<ShieldInfo>().DelWeaponDurable();
                break;
            case 5:
                weapons[_num].GetComponent<CymbalsInfo>().DelWeaponDurable();
                break;
            default:
                break;
        }
    }
}
