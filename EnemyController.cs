using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public int enemyhp;
    public int enemyatk;
    public static EnemyController instance;
    public GameObject crystalToSpawn;

    private void Awake()
    {
        instance = this;
    }

    public void hurtEnemy(int playeratk)
    {
        enemyhp -= playeratk;
        if (enemyhp <= 0)
        {
            Destroy(gameObject);
            Instantiate(crystalToSpawn, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hurtEnemy(PlayerController.instance.playeratk);
        }
            
    }

}
