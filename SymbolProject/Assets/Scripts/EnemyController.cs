using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int enemyhp;
    public GameObject crystalToSpawn;

    //敵は死んでから、クリスタルドロップされる
    public void hurtEnemy(int playeratk)
    {
        enemyhp -= playeratk;
        if (enemyhp <= 0)
        {
            Destroy(gameObject);
            Instantiate(crystalToSpawn, transform.position, Quaternion.identity);
        }

    }
}
