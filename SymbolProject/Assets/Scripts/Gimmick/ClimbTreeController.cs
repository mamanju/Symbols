using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbTreeController : MonoBehaviour
{
    [SerializeField]
    private float fadeStartTime;

    [SerializeField]
    private GameObject climbPos;

    private bool climbFlag = false;

    public bool ClimbFlag {
        get { return climbFlag; }
        set { climbFlag = value; }
    }

    [SerializeField]
    private GameObject climbPos;
    // Start is called before the first frame update
    void Start()
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
