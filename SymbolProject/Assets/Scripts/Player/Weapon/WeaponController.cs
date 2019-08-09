using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    // 攻撃力、耐久
    protected int attack;
    protected int durable;
    protected int durable_max;

    // 武器のID
    protected int weaponID;

    #region プロパティ
    // 攻撃力のプロパティ
    public int Attack
    {
        get { return attack; }
    }

    // 防御力のプロパティ
    public int Durable
    {
        get { return durable; }
    }

    public int Durable_max
    {
        get { return durable_max; }
    }

    // 武器のIDのプロパティ
    public int WeaponID
    {
        get { return weaponID; }
    }

    #endregion

    void Update()
    {

    }

    /// <summary>
    /// 耐久値が減る関数
    /// </summary>
    /// <param name="_durable">減少値</param>
    public void BreakWeaponCheck(int _durable)
    {
        // 耐久値が0以下なら出てくる処理
        if(durable <= 0) 
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
