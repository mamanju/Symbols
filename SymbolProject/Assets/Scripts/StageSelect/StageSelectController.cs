using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージセレクトコントローラー
/// </summary>
public class StageSelectController : MonoBehaviour
{
    public void ChangeGame() {
        SceneController.Instance.ChangeScene(SceneController.SceneName.StageFirst);
    }
}
