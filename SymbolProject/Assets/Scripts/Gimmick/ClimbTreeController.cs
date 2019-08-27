using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 木を登る処理
/// </summary>
public class ClimbTreeController : MonoBehaviour
{
    [SerializeField]
    private float fadeStartTime;

    [SerializeField]
    private GameObject climbPos;

    private bool climbFlag = false;

    /// <summary>
    /// 木を登る
    /// </summary>
    public void Climb(GameObject player)
    {
        if (!climbFlag)
        {
            StartCoroutine(ClimbCoroutine(player));
        }
    }

    private IEnumerator ClimbCoroutine(GameObject player)
    {
        climbFlag = true;
        // アニメーション再生
        // 再生から指定された時間後、フェードイン
        FadePanelManager.instance.FadeIn();
        yield return new WaitForSeconds(fadeStartTime);
        player.transform.position = climbPos.transform.position;
        FadePanelManager.instance.FadeOut();
        yield return new WaitForSeconds(fadeStartTime);
        climbFlag = false;
        yield return null;
    }
}
