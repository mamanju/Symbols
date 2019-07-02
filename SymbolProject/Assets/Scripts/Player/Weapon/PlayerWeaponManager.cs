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
        nowWeapon = Instantiate(weapons[0]);
        nowWeapon.transform.parent = this.transform;
        nowWeapon.transform.localPosition = Vector3.zero;
    }

    public void WeaponObjChange(int _weaponNum)
    {
        nowWeapon.SetActive(false);
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject m_searchObj = this.transform.GetChild(i).gameObject;            
            bool haveWeapon = weapons[_weaponNum].GetComponent<WeaponInfo>().weaponList
                == m_searchObj.GetComponent<WeaponInfo>().weaponList;
            if (haveWeapon)
            {
                m_searchObj.gameObject.SetActive(true);
                nowWeapon = m_searchObj;
                return;
            }
        }
        nowWeapon = Instantiate(weapons[_weaponNum]);
        nowWeapon.transform.parent = this.transform;
        nowWeapon.transform.localPosition = Vector3.zero;
    }
}
