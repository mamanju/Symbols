using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 木の成長
/// </summary>
public class GrowTreeController : MonoBehaviour
{
    [SerializeField]
    private GameObject Seedling;
    [SerializeField]
    private GameObject smokeEffect;
    [SerializeField]
    private GameObject growTree;

    private bool growFlag = false;


    private float growTime;

    /// <summary>
    /// 木の成長
    /// </summary>
    public void GrowTree()
    {
        if (!growFlag)
        {
            growFlag = true;
            StartCoroutine(GrowTreeCoroutine());
        }
    }

    /// <summary>
    /// 木の成長コルーチン
    /// </summary>
    private IEnumerator GrowTreeCoroutine()
    {
        // 苗の成長
        // 煙エフェクト発生、苗の成長をストップ
        // 成長した木を生成、苗を削除
        // 成長した木を大きく
        yield return null;
    }
}
