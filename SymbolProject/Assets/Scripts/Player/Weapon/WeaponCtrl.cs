using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCtrl : MonoBehaviour
{
    // 攻撃力、耐久
    protected int attack;
    protected int durable;

    // 武器のID
    protected string weaponID = "";

    #region プロパティ
    // 攻撃力のプロパティ
    public int Attack {
        get { return attack; }
        set { attack = value; }
    }

    // 防御力のプロパティ
    public int Durable {
        get { return durable; }
    }

    // 武器のIDのプロパティ
    public string WeaponID {
        get { return weaponID; }
    }

    #endregion

    void Start()
    {
        
    }

    /// <summary>
    /// 耐久値が減る関数
    /// </summary>
    /// <param name="_durable">減少値</param>
    public void BreakWeaponCheck(int _durable)
    {
        // 耐久値が0以下なら出てくる処理
        if(durable < 0) 
        {
            return;
        }

        // 耐久値が減る処理
        durable -= _durable;

        // 武器の耐久値が0になったら値を０にする
        if (durable <= 0) 
        {
            durable = 0;
        }
    }
}
