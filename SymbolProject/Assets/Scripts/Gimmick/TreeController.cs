using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//木のギミックの処理のスクリプト
public class TreeController : MonoBehaviour
{
    [SerializeField]
    private int health = 1;
    private Vector3 fallRotate = new Vector3(90,0,0);
    private float treeRotate;
    private float speed = 0.7f;
    private float step;

    private void Update() {
        step = speed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.M)) {
            Cut();
        }
    }

    public void Cut() {
        StartCoroutine(CutTree());
    }

    /// <summary>
    /// 倒木処理
    /// </summary>
    public IEnumerator CutTree()
    {
        while(step < 1) {
            Vector3 defaultRot = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(90f, 0, 0), step);
            yield return null;
        }
        
    }
}
