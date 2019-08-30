using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollider : MonoBehaviour
{
    //プレイヤーにぶつかったものがCrystalだったら10個まで取得
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Crystal")
        {
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                if (TutorialController.instance.CrystalGet == false)
                {
                    TutorialController.instance.CrystalGet = true;
                }
                else if (TutorialController.instance.CrystalGet_2 == true)
                {
                    TutorialController.instance.CrystalGet_2 = true;
                }
            }

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
