using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//プレイヤーのスクリプト（敵の反応、ギミックの反応。。。）
public class PlayerController : MonoBehaviour
{
    public int playerhp;
    public int playeratk;
    public static PlayerController instance;
    public bool haveAxe = false;

    private void Awake()
    {
        instance = this;
    }

    public void hurtPlayer(int enemyatk)
    {
        playerhp -= enemyatk;
        if (playerhp <= 0)
        {
            SceneManager.LoadScene("EnemyScene");
        }

    }

    public void PickupItem()
    {

    }

   

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            hurtPlayer(EnemyController.instance.enemyatk);
        }
        if (other.tag == "FireTrap")
        {
            playerhp--;
        }
        
        
    }
   
}
