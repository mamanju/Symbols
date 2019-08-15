using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    private Animator bossAnime;

    // ボスアニメーション用の変数
    private string key_Speed = "Speed";
    private string key_Attack = "Attack";
    private string key_Barrier = "Barrier";

    void Start()
    {
        bossAnime = GetComponent<Animator>();
    }

    void Update()
    {
        // ボスの攻撃時に呼ぶようにする( Attack )
        // デバッグ用にキーでアニメーション起動
        if (Input.GetKeyDown(KeyCode.Q))
        {
            bossAnime.SetTrigger(key_Attack);
        }

        // ボスのバリア攻撃時に呼ぶようにする( Barrier )
        // デバッグ用にキーでアニメーション起動
        if (Input.GetKeyDown(KeyCode.W))
        {
            bossAnime.SetTrigger(key_Barrier);
        }

        // ボスのスピードが一定値以上の場合歩くようにする( Walk )
        // デバッグ用にキーでアニメーション起動
        if (Input.GetKeyDown(KeyCode.E))
        {
            bossAnime.SetFloat(key_Speed, 1.1f);
        }

        // ボスのスピードが一定値以上の場合歩くようにする( Walk )
        // デバッグ用にキーでアニメーション起動
        if (Input.GetKeyDown(KeyCode.T))
        {
            bossAnime.SetFloat(key_Speed, 0.0f);
        }
    }
}
