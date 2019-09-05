using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableTrapSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject Fires;

    private float deleteDistance = 2f;
    private float deleteTime = 2f;

    public void StopFire() {
        GetComponent<AudioSource>().Play();

        for(int i = 0; i < Fires.transform.childCount; i++) {
            if (Fires.transform.GetChild(i).GetChild(0).GetComponent<VariableTrap>()) {
                Debug.Log("OK");
                Destroy(Fires.transform.GetChild(i).GetChild(0).GetComponent<VariableTrap>().gameObject);
            }
            Fires.transform.GetChild(i).GetChild(0).GetComponent<VariableTrap>().FireOff();
        }

        
    }

    private IEnumerator DeleteCrystal()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject crystal = transform.GetChild(i).gameObject;
            crystal.GetComponent<Rigidbody>().isKinematic = false;
        }
        yield return new WaitForSeconds(deleteDistance);
        
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        yield return null;
    }
}
