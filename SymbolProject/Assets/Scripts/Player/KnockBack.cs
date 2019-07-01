using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField]
    private float knockbackTime = 0.05f;
    private bool knockbackFlag = false;

    private float knockbackTimeReset;

    Rigidbody playerRb;


    private float forceMgmt = 2.0f;
    private Vector3 speedForce;


    // Start is called before the first frame update
    void Start()
    {
        knockbackTimeReset = knockbackTime;
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (knockbackFlag == true)
        {
            knockbackTime -= Time.deltaTime;
            if (knockbackTime <= 0)
            {
                knockbackTime = knockbackTimeReset;
                knockbackFlag = false;
            }
        }

    }
}
