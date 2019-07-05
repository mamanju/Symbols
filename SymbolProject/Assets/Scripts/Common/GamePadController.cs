using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームパッドの情報一覧
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
}

/// <summary>
/// キー全般の入力
/// </summary>
public class GamePadController : SingletonMonoBehaviour<GamePadController> {
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
    /// 十字キー上下
    /// </summary>
    /// <returns></returns>
    public float CrossHorizontal() {
        return Input.GetAxis("CrossKey_H");
    }

    /// <summary>
    /// 十字キー左右
    /// </summary>
    /// <returns></returns>
    public float CrossVertical() {
        return Input.GetAxis("CrossKey_V");
    }

    /// <summary>
    /// Horizontalの値変更と保持
    /// </summary>
    public void SetHorizontal()
    {
        JoyStickAxis[GamePadManager.JoySticks.Horizontal_L] = Input.GetAxis("Horizontal_L");
        JoyStickAxis[GamePadManager.JoySticks.Horizontal_R] = Input.GetAxis("Horizontal_R");
    }

    /// <summary>
    /// Verticalの値変更と保持
    /// </summary>
    public void SetVertical() {
        JoyStickAxis[GamePadManager.JoySticks.Vertical_L] = Input.GetAxis("Vertical_L");
        JoyStickAxis[GamePadManager.JoySticks.Vertical_R] = Input.GetAxis("Vertical_R");
        
    }

    /// <summary>
    /// どのボタンが押されたか
    /// </summary>
    /// <returns></returns>
    public string ButtonPush() {
        if (!Input.anyKeyDown) { return null; }

        if (Input.GetButtonDown("Cross")) {
            return "Cross";
        }
        if (Input.GetButtonDown("Circle")) {
            return "Circle";
        }
        if (Input.GetButtonDown("Square")) {
            return "Square";
        }
        if (Input.GetButtonDown("Triangle")) {
            return "Triangle";
        }
        if (Input.GetButtonDown("L1")) {
            return "L1";
        }
        if (Input.GetButtonDown("R1")) {
            return "R1";
        }
        if (Input.GetButtonDown("L2")) {
            Debug.Log("L2");
            return "L2";
        }
        if (Input.GetButtonDown("R2")) {
            Debug.Log("R");
            return "R2";
        }
        return null;

        //foreach (var btn in System.Enum.GetValues(typeof(GamePadManager.Buttons)).ToString()) {
        //    if (Input.GetButtonDown(btn.ToString())) {
        //        Debug.Log(Input.inputString);
        //    }
        //}
    }

    void Update() {
        SetHorizontal();
        SetVertical();
        ButtonPush();
    }
}
