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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground")
        {
            player.GetComponent<PlayerController>().GroundFlag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player.GetComponent<PlayerController>().GroundFlag = false;
    }
}
