using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 倒木ギミックの処理全般
/// </summary>
public class CutTreeController : MonoBehaviour
{
    // 倒れる木
    [SerializeField]
    private GameObject fallTree;

    // 倒木後、消滅する木
    [SerializeField]
    private GameObject destroyTree;

    // 倒れる速さ
    [SerializeField]
    private float fallSpeed;

    private bool fallFlag = false;

    public bool SetFallFlag
    {
        set { fallFlag = value; }
    }

    void Update()
    {
        if(fallFlag){
            CutTree();
        }        
    }

    /// <summary>
    /// 倒木
    /// </summary>
    public void CutTree()
    {
        if (!fallFlag) { return; }
        fallFlag = false;
        StartCoroutine(CutreeCoroutine());
        transform.parent.GetComponent<BoxCollider>().enabled = false;
    }

    /// <summary>
    /// 倒木コルーチン
    /// </summary>
    public IEnumerator CutreeCoroutine()
    {
        float beforeRotate = 0;
        // 直角になるまで徐々に倒す
        while (beforeRotate - fallTree.transform.localEulerAngles.x <= 0)
        {       
            beforeRotate = fallTree.transform.localEulerAngles.x;
            float treeRotate = fallTree.transform.rotation.x;
            float rotateX = Mathf.MoveTowards(treeRotate, treeRotate + fallSpeed, fallSpeed * Time.deltaTime);
            fallTree.transform.Rotate(rotateX, 0, 0);
            yield return null;
        }
        Destroy(destroyTree);
    }
}
