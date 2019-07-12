using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム中に使用する情報などの管理
/// </summary>
public class GameStateController : MonoBehaviour
{
    /// <summary>
    /// どの状態か
    /// </summary>
    public enum PlayerState
    {
        Action,
        Synthesis,
        Pause,
    }

    public PlayerState PState;

    /// <summary>
    /// プレイヤー
    /// </summary>
    public GameObject PlayerObject;
}
