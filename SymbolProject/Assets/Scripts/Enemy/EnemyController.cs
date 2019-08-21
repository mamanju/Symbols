using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エネミーコントローラー
/// </summary>
public class EnemyController : EnemyManager
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

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
        if (Health - damage <= 0)
        {
            DropCrystal();
            Destroy(gameObject);
            return gameObject;
        }
        Health -= damage;
        anim.SetTrigger("Damage");
        return null;
    }

    /// <summary>
    /// クリスタルドロップ
    /// </summary>
    private void DropCrystal() {
        string path = "Prefabs/Crystal/MCrystal/Prefab/MCrystal_" + crystal;
        GameObject cry = Instantiate(Resources.Load<GameObject>(path), transform);
        cry.transform.position = transform.position;
        cry.transform.SetParent(transform.parent);
        cry.GetComponent<CapsuleCollider>().enabled = false;
    }
}
