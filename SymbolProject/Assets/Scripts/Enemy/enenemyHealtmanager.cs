using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enenemyHealtmanager : MonoBehaviour
{
    public int healt = 1;
    public int soundToPlay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            hurtEnemy();
        }
        
    }
    void hurtEnemy()
    {
        healt--;
        if (healt <= 0)
        {
            Destroy(gameObject);
            SoundManager.instance.playSfx(soundToPlay);
        }

    }
    
}
