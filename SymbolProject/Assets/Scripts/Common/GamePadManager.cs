using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームパッドの入力信号を発信するクラス
/// </summary>
public static class GamePadManager
{
    /// <summary>
    /// ジョイスティック一覧
    /// </summary>
    public enum JoySticks
    {
        Horizontal_L,
        Horizontal_R,
        Vertical_L,
        Vertical_R,
    }

    /// <summary>
    /// ボタン一覧(Button)
    /// </summary>
    public enum Buttons
    {
        Square,
        Cross,
        Circle,
        Triangle,
        L1,
        R1,
        L2,
        R2,
        Share,
        Option,
        PSButton,
        TrackPad,
    }

    /// <summary>
    /// ジョイスティック(Axis)
    /// </summary>
    //public static Dictionary<JoySticks, float> JoyStickAxis = new Dictionary<JoySticks, float>()
    //{
    //    { JoySticks.Horizontal_L,0 },
    //    { JoySticks.Horizontal_R,0 },
    //    { JoySticks.Vertical_L,0 },
    //    { JoySticks.Vertical_R,0 },
    //};

}

public class GamePadController : MonoBehaviour {
    /// <summary>
    /// ジョイスティック(Axis)
    /// </summary>
    public Dictionary<GamePadManager.JoySticks, float> JoyStickAxis = new Dictionary<GamePadManager.JoySticks, float>()
    {
        { GamePadManager.JoySticks.Horizontal_L,0 },
        { GamePadManager.JoySticks.Horizontal_R,0 },
        { GamePadManager.JoySticks.Vertical_L,0 },
        { GamePadManager.JoySticks.Vertical_R,0 },
    };

    /// <summary>
    /// Horizontalの値変更と保持
    /// </summary>
    /// <param name="stick"></param>
    /// <param name="horizontal"></param>
    public void SetHorizontal(GamePadManager.JoySticks stick,float horizontal)
    {
        // if()
    }
}
