using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowAction : MonoBehaviour
{
    [SerializeField]
    private float _startSpeed;
   
    public void Shot(Vector3 player)
    {
        GetComponent<Rigidbody>().AddForce(player *_startSpeed, ForceMode.Impulse);
    }

    public void OnCollisionEnter(Collision col) {
        if(col.gameObject.tag == "FireSwitch") {
            col.gameObject.GetComponent<VariableTrapSwitch>().StopFire();
        }
        Destroy(transform.parent.gameObject);
    }

}