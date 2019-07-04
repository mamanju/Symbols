using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵のステータス
/// </summary>
public class EnemyManager : MonoBehaviour
{
    /// <summary>
    /// クリスタルリスト
    /// </summary>
    [SerializeField]
    private enum Crystals
    {
        Circle,
        Triangle,
        Plus,
        Stick,
        Parentheses,
    }

    /// <summary>
    /// 体力
    /// </summary>
    [SerializeField]
    private int health;

    /// <summary>
    /// 攻撃力
    /// </summary>
    [SerializeField]
    private int attack;

    /// <summary>
    /// 攻撃スピード
    /// </summary>
    [SerializeField]
    private float attackSpeed;

    /// <summary>
    /// 移動スピード
    /// </summary>
    [SerializeField]
    private float moveSpeed;

    /// <summary>
    /// 攻撃したかどうか
    /// </summary>
    private bool attackFlag = false;

    /// <summary>
    /// 攻撃したかどうかの参照
    /// </summary>
    public bool AttackFlag {
        get { return attackFlag; }
        set { attackFlag = value; }
    }
    /// <summary>
    /// 攻撃力の取得
    /// </summary>
    public int GetAttack {
        get { return attack; }
    }

    /// <summary>
    /// 体力の参照
    /// </summary>
    public int Health {
        get { return health; }
        set { health = value; }
    }
}
