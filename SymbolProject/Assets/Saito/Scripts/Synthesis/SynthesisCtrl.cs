using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynthesisCtrl : MonoBehaviour
{
    //合成するところに入力されたクリスタル
    private GameObject[] matlCrystals = new GameObject[5];

    //合成中フラッグ
    private bool startSynthesis = false;

    //レシピリスト
    //槍
    private readonly int[] spear = { 0, 0, 0, 1, 2 };
    //斧
    private readonly int[] ax = { 0, 0, 0, 1, 3 };
    //盾
    private readonly int[] shield = { 0, 1, 1, 1, 1 };

    //入力された素材(配列)
    private int matlCount = 0;
    private int[] inputMatl = new int[5];
    private int changeFlag = 0;
    private bool changeFin = false;

    //なぜかfor文抜けるので対策用
    private bool setFlag = false;

    //合成成功した際の送り先
    [SerializeField]
    private GameObject synthesisCrystal;

    //個数管理用のオブジェクト
    [SerializeField]
    private GameObject weaponBoxes;
    private GameObject[] weaponBox;
    private int weaponBoxCount;

    private int synthesisCount;

    // Start is called before the first frame update
    void Start()
    {
        weaponBoxCount = weaponBoxes.transform.childCount;
        weaponBox = new GameObject[weaponBoxCount];
        for (int i = 0; i < weaponBoxCount; i++)
        {
            weaponBox[i] = weaponBoxes.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            startSynthesis = true;
        }

        //入力された素材クリスタルの取得
        if (!startSynthesis) { return; }

        matlCount = this.transform.childCount;


        for (int i = 0; i < matlCount; i++)
        {
            matlCrystals[i] = transform.GetChild(i).gameObject;
            inputMatl[i] = (int)matlCrystals[i].GetComponent<MatlInfo>().matlList;
            setFlag = true;
        }

        if (!setFlag) { return; }
        
        //素材クリスタルの並び替え(昇順)
        while (changeFin == false)
        {
            changeFlag = 0;
            for (int i = 0; i < 4; i++)
            {
                if (inputMatl[i] > inputMatl[i + 1])
                {
                    int a = inputMatl[i];
                    inputMatl[i] = inputMatl[i + 1];
                    inputMatl[i + 1] = a;
                    changeFlag++;
                }
            }
            if (changeFlag == 0) { changeFin = true; };
        }
        Synthesis(inputMatl[0], inputMatl[1],
              inputMatl[2], inputMatl[3], inputMatl[4]);
        EndSynthesis();
    }

    public void Synthesis(int a, int b, int c, int d, int e)
    {
        //レシピNo.1
        if (a == spear[0] && b == spear[1] &&
            c == spear[2] && d == spear[3] && e == spear[4])
        {
            SynthesisCount((int)WeaponInfo.WeaponList.spear);
            if (synthesisCount >= 5)
            {
                Debug.Log("所持上限を超えています");
                return;
            }
            Debug.Log("槍");
            synthesisCrystal.GetComponent<WeaponInfo>().weaponList = WeaponInfo.WeaponList.spear;
        }
        else if (a == ax[0] && b == ax[1] && c == ax[2] && d == ax[3] && e == ax[4])
        {
            SynthesisCount((int)WeaponInfo.WeaponList.ax);
            if (synthesisCount >= 5)
            {
                Debug.Log("所持上限を超えています");
                return;
            }
            Debug.Log("斧");
            synthesisCrystal.GetComponent<WeaponInfo>().weaponList = WeaponInfo.WeaponList.ax;
        }
        else if (a == shield[0] && b == shield[1] && c == shield[2] && d == shield[3] && e == shield[4])
        {
            SynthesisCount((int)WeaponInfo.WeaponList.shield);
            if (synthesisCount >= 5)
            {
                Debug.Log("所持上限を超えています");
                return;
            }
            Debug.Log("盾");
            synthesisCrystal.GetComponent<WeaponInfo>().weaponList = WeaponInfo.WeaponList.shield;
        }
        //レシピNo.0は失敗
        else
        {
            Debug.Log("失敗");
        }
    }

    public void SynthesisCount(int a)
    {
        for (int i = 0; i < weaponBox.Length; i++)
        {
            if ((int)weaponBox[i].GetComponent<WeaponInfo>().weaponList == a)
            {
                synthesisCount++;
            }
        }
    }

    //ゲームオブジェクトの削除
    public void EndSynthesis()
    {
        for (int i = 0; i < matlCount; i++)
        {
            matlCrystals[i].GetComponent<MatlInfo>().matlList = MatlInfo.MatlList.empty;
            inputMatl[i] = 0;
        }
        changeFin = false;
        startSynthesis = false;
        synthesisCount = 0;
    }
}