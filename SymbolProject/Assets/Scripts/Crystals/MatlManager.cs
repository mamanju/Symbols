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
        nowMatl = new int[] { 10, 1, 6, 2 };
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

    public void HaveCrystal()
    {
        for(int i = 0; i < matlBox.Length; i++)
        {

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
