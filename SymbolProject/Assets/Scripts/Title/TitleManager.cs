using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルマネージャー
/// </summary>
public class TitleManager : MonoBehaviour
{
    /// <summary>
    /// ステージセレクトへ遷移
    /// </summary>
    public void MoveSelect() {
        SceneController.Instance.ChangeScene("StageSelect");
    }
}
