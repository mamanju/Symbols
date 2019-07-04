using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルマネージャー
/// </summary>
public class TitleManager : MonoBehaviour
{
    private void Update() {
        if (Input.GetButtonDown("Circle")) {
            MoveSelect();
        }
    }
    /// <summary>
    /// ステージセレクトへ遷移
    /// </summary>
    public void MoveSelect() {
        SceneController.Instance.ChangeScene("StageFirst");
    }


    //private float Horizontal_L;
    //private float Vertical_L;
    //private float Horizontal_R;
    //private float Vertical_R;
    //private void Update()
    //{
    //    Horizontal_L = Input.GetAxis("Horizontal_L");
    //    Vertical_L = Input.GetAxis("Vertical_L");
    //    Horizontal_R = Input.GetAxis("Horizontal_R");
    //    Vertical_R = Input.GetAxis("Vertical_R");

    //    // if () { return; }

    //    Debug.Log("Horizon_L = " + Horizontal_L);
    //    Debug.Log("Horizon_R = " + Horizontal_R);
    //    Debug.Log("vertical_L = " + Vertical_L);
    //    Debug.Log("vertical_R = " + Vertical_R);
        
    //    if (Input.GetButtonDown("Cross"))
    //    {
    //        Debug.Log("×");
    //    }
    //    if (Input.GetButtonDown("Circle"))
    //    {
    //        Debug.Log("〇");
    //    }
    //    if (Input.GetButtonDown("Square"))
    //    {
    //        Debug.Log("□");
    //    }
    //    if (Input.GetButtonDown("Triangle"))
    //    {
    //        Debug.Log("△");
    //    }
    //    if (Input.GetButtonDown("L1"))
    //    {
    //        Debug.Log("L1");
    //    }
    //    if (Input.GetButtonDown("R1"))
    //    {
    //        Debug.Log("R1");
    //    }
        
    //}
}
