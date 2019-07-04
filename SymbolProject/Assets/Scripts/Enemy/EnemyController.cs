using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エネミーコントローラー
/// </summary>
public class EnemyController : EnemyManager
{
    /// <summary>
    /// 攻撃処理
    /// </summary>
    public void Attacking() {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine() {
        AttackFlag = true;
        yield return new WaitForSeconds(1f);
        AttackFlag = false;
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="damage"></param>
    public GameObject Damage(int damage)
    {
        Health -= damage;
        if (Health - damage <= 0)
        {
            //Instantiate(crystalToSpawn, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return gameObject;
        }
        return null;
    }

    /// <summary>
    /// クリスタルドロップ
    /// </summary>
    private void DropCrystal() {

    }

    private void OnTriggerEnter(Collider other)
    {
         
    }

}
