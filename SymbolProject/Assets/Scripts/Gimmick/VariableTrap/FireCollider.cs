using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollider : MonoBehaviour
{
    public void OnCollisionEnter(Collision col) {
        if (col.gameObject.GetComponent<PlayerStatus>()) {
            col.gameObject.GetComponent<PlayerStatus>().DownHP(1);
        }
    }
}
