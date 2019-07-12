using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = this.transform.parent.gameObject;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            player.GetComponent<PlayerCtrl>().GroundFlag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player.GetComponent<PlayerCtrl>().GroundFlag = false;
    }
}
