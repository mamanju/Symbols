using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//斧のスクリプト
public class Axe : MonoBehaviour
{
    public Collider axe;
    private bool hit;
    public float timer;
    private float counter;
    // Start is called before the first frame update
    void Start()
    {
        counter = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if(hit)
        {
            counter -= Time.deltaTime;
            if(counter<=0)
            {
                hit = false;
                axe.enabled = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            hit = true;
            axe.enabled = true;
            counter = timer;
            }
    } 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.haveAxe = true;
            transform.SetParent(PlayerController.instance.transform);
            GetComponent<Rigidbody>().isKinematic=true;
            if (!hit)
            {
                axe.enabled = false;
            }
           
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        PlayerController.instance.haveAxe = false;
    }

}
