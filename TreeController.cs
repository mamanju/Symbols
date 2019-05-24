using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//木のギミックの処理のスクリプト
public class TreeController : MonoBehaviour
{
    public int health=1;
    public GameObject objectToSpawn;

    

    public void CutTree()
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(objectToSpawn, transform.position, transform.rotation = Quaternion.Euler(90f,0f,0f)); 
        } 
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Axe"&&PlayerController.instance.haveAxe)
        {
                CutTree();
        }
    }
}
