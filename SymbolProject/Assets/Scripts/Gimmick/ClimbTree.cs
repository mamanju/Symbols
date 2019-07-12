using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 木を登る処理
/// </summary>
public class ClimbTree : MonoBehaviour
{
    [SerializeField]
    private GameObject climbPos;

    /// <summary>
    /// 木を登る
    /// </summary>
    public void Climb(GameObject player)
    {
        StartCoroutine(ClimbCoroutine(player));
    }

    public IEnumerator ClimbCoroutine(GameObject player)
    {

        // アニメーション再生
        // 再生から指定された時間後、フェードイン
        player.transform.position = climbPos.transform.position;
        // フェードアウト
        yield return null;
    }
}
