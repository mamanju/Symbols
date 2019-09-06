using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator slimeAnime;

    // スライムアニメーション用の変数
    private string key_Speed = "Speed";

    // Start is called before the first frame update
    void Start()
    {
        slimeAnime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
