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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            GrowTree();
        }
    }

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
        // 苗の成長
        while (growTime < 1)
        {
            Seedling.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            growTime += 0.01f;
            yield return null;
        }
        // 煙エフェクト発生、苗の成長をストップ
        GameObject smoke = Instantiate(smokeEffect);
        smoke.transform.position = Seedling.transform.position;
        // 成長した木を生成、苗を削除
        GameObject tree = Instantiate(growTree);
        tree.transform.position = Seedling.transform.position;
        Destroy(Seedling);
        // 成長した木を大きく
        while(growTree.transform.localScale.x < 1)
        {
            growTree.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            yield return null;
        }
        Destroy(smoke);
    }
}
