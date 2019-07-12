using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTipInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("敵に当たった");
        }else if (collision.gameObject.tag == "Gimmick")
        {
            Debug.Log("ギミック発動");
        }else
        {
            Debug.Log("何かに当たった");
        }
        Destroy(gameObject);
    }
}
