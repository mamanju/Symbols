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

    [SerializeField]
    private float fadeStartTime;

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
        yield return new WaitForSeconds(fadeStartTime);
        FadePanelManager.instance.FadeIn();
        player.transform.position = climbPos.transform.position;
        // フェードアウト
        FadePanelManager.instance.FadeOut();
        yield return null;
    }
}
