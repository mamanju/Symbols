using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    MatlManager matlManager;
    WeaponManager weaponManager;

    //プレイヤーにぶつかったものがCrystalだったら10個まで取得
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Crystal")
        {
            MatlInfo matlInfo = other.GetComponent<MatlInfo>();
            matlManager = this.GetComponent<MatlManager>();
            if (matlManager.NowMatl[(int)matlInfo.matlList] >= 10)
            {
                Debug.Log("所持上限を超えています");
                return;
            }
            else
            {
                matlManager.NowMatl[(int)matlInfo.matlList]++;
                Destroy(other.gameObject);
            }
        }

        if (other.tag == "WeaponCrystal")
        {
            WeaponInfo weaponInfo = other.GetComponent<WeaponInfo>();
            weaponManager = this.GetComponent<WeaponManager>();
            if (weaponManager.NowWeapon[(int)weaponInfo.weaponList] >= 5)
            {
                Debug.Log("所持上限を超えています");
                return;
            }
            else
            {
                weaponManager.NowWeapon[(int)weaponInfo.weaponList]++;
                Destroy(other.gameObject);
            }
        }
    }
}
