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


    private float growTime;

    /// <summary>
    /// 木の成長
    /// </summary>
    public void GrowTree()
    {
        StartCoroutine(GrowTreeCoroutine());
    }

    /// <summary>
    /// 木の成長コルーチン
    /// </summary>
    private IEnumerator GrowTreeCoroutine()
    {
        // 煙エフェクト発生
        GameObject smoke = Instantiate(smokeEffect);
        smoke.transform.position = Seedling.transform.position;

        // 苗の成長
        while (growTime < 1)
        {
            Seedling.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            growTime += 0.01f;
            yield return null;
        }
        // 苗の成長をストップ


        // 成長した木を生成、苗を削除
        growTree.SetActive(true);
        Destroy(Seedling);
        // 成長した木を大きく
        while(growTime < 2)
        {
            growTree.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            growTime += 0.01f;
            yield return null;
        }
        Destroy(smoke);
    }
}
