using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableTrapSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject Fires;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopFire() {
        for(int i = 0; i < Fires.transform.childCount; i++) {
            Fires.transform.GetChild(i).GetComponent<VariableTrap>().ActiveFlag = true;
        }
    }
}
