using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    //プレイヤーにぶつかったものがCrystalだったら10個まで取得
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Crystal")
        {
            MatlInfo matlInfo = other.GetComponent<MatlInfo>();
            other.GetComponent<CapsuleCollider>().enabled = false;
            if (MatlManager.NowMatl[(int)matlInfo.matlList] >= 10)
            {
                Debug.Log("所持上限を超えています");
                other.GetComponent<CapsuleCollider>().enabled = true;
                return;
            }
            else
            {
                Destroy(other.gameObject);
                MatlManager.NowMatl[(int)matlInfo.matlList]++;
            }
        }

        if (other.tag == "WeaponCrystal")
        {
            WeaponInfo weaponInfo = other.GetComponent<WeaponInfo>();
            if (WeaponManager.NowWeapon[(int)weaponInfo.weaponList] >= 5)
            {
                Debug.Log("所持上限を超えています");
                return;
            }
            else
            {
                WeaponManager.NowWeapon[(int)weaponInfo.weaponList]++;
                Destroy(other.gameObject);
            }
        }
    }
}
