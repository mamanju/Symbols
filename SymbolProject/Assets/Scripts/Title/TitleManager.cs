using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルマネージャー
/// </summary>
public class TitleManager : MonoBehaviour
{
    private void Update() {
        if (Input.GetButtonDown("Circle") || Input.GetKeyDown(KeyCode.O)) {
            MoveSelect();
        }
    }
    /// <summary>
    /// ステージセレクトへ遷移
    /// </summary>
    public void MoveSelect() {
        SceneController.Instance.ChangeScene(SceneController.SceneName.Tutorial);
    }
}
