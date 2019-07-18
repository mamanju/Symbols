using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableTrapSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject Fires;

    public void StopFire() {
        for(int i = 0; i < Fires.transform.childCount; i++) {
            if (Fires.transform.GetChild(i).GetChild(0).GetComponent<VariableTrap>()) {
                Debug.Log("OK");
                Destroy(Fires.transform.GetChild(i).GetChild(0).GetComponent<VariableTrap>().gameObject);
            }
            Fires.transform.GetChild(i).GetChild(0).GetComponent<VariableTrap>().FireOff();
        }
    }
}
