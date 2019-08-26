using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CymbalsTimeManager : MonoBehaviour
{
    [SerializeField]
    private WeaponAtaccks weaponAtaccks;
    
    private float stunTime = 5;

    // Update is called once per frame
    void Update()
    {
        if (weaponAtaccks.StunFlag == true)
        {
                stunTime -= Time.unscaledDeltaTime;
        }
        if (stunTime <= 0)
        {
            weaponAtaccks.StunFlag = false;
            stunTime = 5;
            weaponAtaccks.CymbalsEnd();
        }
    }
}
