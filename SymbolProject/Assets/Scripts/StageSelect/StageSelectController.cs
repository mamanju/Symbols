using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージセレクトコントローラー
/// </summary>
public class StageSelectController : MonoBehaviour
{
    public void ChangeGame(string name) {
        SceneController.Instance.ChangeScene(name);
    }
}
