using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatlManager : MonoBehaviour
{
    private List<int> nowMatlList = new List<int>(30);

    [SerializeField]
    private GameObject matlBoxes;

    private GameObject[] matlBox = new GameObject[30];

    private int matlCount;

    //リストの初期化
    void Start()
    {
        for (int i = 0; i < matlBox.Length; i++)
        {
            matlBox[i] = matlBoxes.transform.GetChild(i).gameObject;
        }
        //Debug用
        nowMatlList = new List<int> { 1, 1, 1, 1, 1, 1, 2, 3, };
    }

    //0を消す処理➤要らないかも
    void Update()
    {
        for (int i = 0; i < nowMatlList.Count; i++)
        {
            if ( nowMatlList[i] == 0)
            {
                nowMatlList.RemoveAt(i);
            }
        }
    }

    //プレイヤーにぶつかったものがCrystalだったら10個まで取得
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Crystal"))
        {
            matlCount = 0;
            for (int i = 0; i < nowMatlList.Count; i++)
            {
                if (nowMatlList[i] == (int)other.GetComponent<MatlInfo>().matlList)
                {
                    matlCount++;
                }
            }
            if (matlCount >= 10)
            {
                Debug.Log("所持上限を超えています");
                return;
            }
            nowMatlList.Add((int)other.GetComponent<MatlInfo>().matlList);
        }
    }

    public void AddCrystal(int i)
    {
        nowMatlList.Add(i);
    }

    //今持っている素材クリスタを合成画面の選択画面に送る
    public void HaveCrystal()
    {
        //リストの中身全部
        for (int i = 0; i < nowMatlList.Count; i++)
        {
            //素材ボックスのmatlListをリストの中の値に変更する
            matlBox[i].GetComponent<MatlInfo>().matlList
                = ((MatlInfo.MatlList)Enum.ToObject(typeof(MatlInfo.MatlList), nowMatlList[i]));
        }
        nowMatlList.Clear();
    }
}
