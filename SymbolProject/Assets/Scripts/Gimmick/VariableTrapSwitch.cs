using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableTrapSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject Fires;

    public void StopFire() {
        for(int i = 0; i < Fires.transform.childCount; i++) {
            Fires.transform.GetChild(i).GetComponent<VariableTrap>().FireOff();
        }
    }
}
