using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatlManager : MonoBehaviour
{
    private int[] nowMatl = new int[4];
    public int[] NowMatl
    {
        get { return nowMatl; }
        set { nowMatl = value; }
    }

    [SerializeField]
    private GameObject matlBoxes;

    private GameObject[] matlBox = new GameObject[4];

    private int matlCount;

    //リストの初期化
    void Start()
    {
        for (int i = 0; i < matlBox.Length; i++)
        {
            matlBox[i] = matlBoxes.transform.GetChild(i).gameObject;
        }
        //Debug用
        nowMatl = new int[] { 20, 1, 1, 0 };
        //nowMatlList = new List<int> { 1, 1, 1, 1, 1, 1, 2, 3, };
    }

    void Update()
    {
        for (int i = 0; i < nowMatl.Length; i++)
        {
            if (nowMatl[i] == 0)
            {
                matlBox[i].GetComponent<MatlInfo>().matlList = MatlInfo.MatlList.empty;
            }
        }
    }

    //プレイヤーにぶつかったものがCrystalだったら10個まで取得
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Crystal"))
        {
            if (nowMatl[(int)other.GetComponent<MatlInfo>().matlList] >= 10)
            {
                Debug.Log("所持上限を超えています");
                return;
            }
            else
            {
                nowMatl[(int)other.GetComponent<MatlInfo>().matlList]++;
            }
        }
    }

    public void HaveCrystal()
    {
        for(int i = 0; i < matlBox.Length; i++)
        {
            Debug.Log(nowMatl[i]);
            if(nowMatl[i] != 0)
            {
                matlBox[i].GetComponent<MatlInfo>().matlList
                    = ((MatlInfo.MatlList)Enum.ToObject(typeof(MatlInfo.MatlList), i));
            }
            else
            {
                matlBox[i].GetComponent<MatlInfo>().matlList
                    = MatlInfo.MatlList.empty;
            }
        }
    }
}
