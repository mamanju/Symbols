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
    private GameObject[] climbPos;

    private bool climbFlag = false;

    /// <summary>
    /// 木を登る
    /// </summary>
    public void Climb(GameObject player)
    {
        GameObject cPos = climbPos[0];
        if (climbFlag) {
            cPos = climbPos[1];
        } else {
            cPos = climbPos[0];
        }
        StartCoroutine(ClimbCoroutine(player,cPos));
    }

    public IEnumerator ClimbCoroutine(GameObject player,GameObject pos)
    {
        // アニメーション再生
        // 再生から指定された時間後、フェードイン
        yield return new WaitForSeconds(fadeStartTime);
        FadePanelManager.instance.FadeIn();
        player.transform.position = pos.transform.position;
        yield return null;
        if (climbFlag) {
            climbFlag = false;
        } else {
            climbFlag = true;
        }
    }
}
