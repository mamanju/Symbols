using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    private Animator bossAnime;

    // ボスアニメーション用の変数
    private string key_Speed = "Speed";         // float
    private string key_Attack = "Attack";       // trigger
    private string key_S_Attack = "SAttack";   // trigger
    private string key_Barrier = "Barrier";     // trigger
    private string key_Damage = "Damage";       // trigger
    private string key_HP = "HP";               // int

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

        // ボスの強攻撃時に呼ぶようにする( S Attack )
        // デバッグ用にキーでアニメーション起動
        if (Input.GetKeyDown(KeyCode.W))
        {
            bossAnime.SetTrigger(key_S_Attack);
        }

        // ボスのバリア攻撃時に呼ぶようにする( Barrier )
        // デバッグ用にキーでアニメーション起動
        if (Input.GetKeyDown(KeyCode.E))
        {
            bossAnime.SetTrigger(key_Barrier);
        }

        // ボスのスピードが一定値以上の場合歩くようにする( Walk )
        // デバッグ用にキーでアニメーション起動
        if (Input.GetKeyDown(KeyCode.T))
        {
            bossAnime.SetFloat(key_Speed, 1.1f);
        }

        // ボスのスピードが一定値以上の場合歩くようにする( Walk )
        // デバッグ用にキーでアニメーション起動
        if (Input.GetKeyDown(KeyCode.Y))
        {
            bossAnime.SetFloat(key_Speed, 0.0f);
        }

        // ボスがダメージを受けた時( Damage )
        // デバッグ用にキーでアニメーション起動
        if (Input.GetKeyDown(KeyCode.U))
        {
            bossAnime.SetTrigger(key_Damage);
        }

        // ボスのHPが0になった時( HP )
        // デバッグ用にキーでアニメーション起動
        if (Input.GetKeyDown(KeyCode.I))
        {
            bossAnime.SetInteger(key_HP, 0);
        }
    }
}
