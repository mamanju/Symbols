using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField]
    private float knockbackTime = 0.05f;
    private bool knockbackFlag = false;
    public bool KnockbackFlag
    {
        get { return knockbackFlag; }
        set { knockbackFlag = value; }
    }
    private bool invincibleFlag = false;
    [SerializeField]
    private float invincibleTime = 0.5f;

    private float knockbackTimeReset;
    private float invincibleTimeReset;

    Rigidbody playerRb;


    private float forceMgmt = 2.0f;
    private Vector3 speedForce;

    [SerializeField]
    private float knockbackPower = 3.0f;

    [SerializeField]
    private float hitJump = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        invincibleTimeReset = invincibleTime;
        knockbackTimeReset = knockbackTime;
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleFlag == true)
        {
            invincibleTime -= Time.deltaTime;
            if (invincibleTime <= 0)
            {
                invincibleTime = invincibleTimeReset;
                invincibleFlag = false;
            }
        }
        if (knockbackFlag == true)  //
        {
            knockbackTime -= Time.deltaTime;
            if (knockbackTime <= 0)
            {
                knockbackTime = knockbackTimeReset;
                knockbackFlag = false;
            }
        }
    }

    public void Knockback()
    {
        invincibleFlag = true;
        knockbackFlag = true;
        Vector3 knockback = new Vector3(-transform.forward.x, hitJump, -transform.forward.z);
        playerRb.AddForce(knockback * knockbackPower, ForceMode.Impulse);
        playerRb.velocity = Vector3.zero;
    }
}
