using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynthesisCtrl : MonoBehaviour
{
    //合成するところに入力されたクリスタル
    private GameObject[] matlCrystals = new GameObject[5];

    //合成中フラッグ
    private bool startSynthesis = false;
    public bool StartSynthesis
    {
        get { return startSynthesis; }
        set { startSynthesis = value; }
    }

    private bool endFlag = false;
    public bool EndFlag
    {
        get { return endFlag; }
        set { endFlag = value; }
    }

    //レシピリスト
    //槍
    private readonly int[] spear = { -1, -1, -1, 0, 1 };
    //斧
    private readonly int[] ax = { -1, -1, -1, 0, 2 };
    //盾
    private readonly int[] shield = { -1, 0, 0, 0, 0 };
    //双剣
    private readonly int[] twinSword = { -1, -1, -1, 0, 0 };
    //シンバル
    private readonly int[] cymbal = { -1, -1, 0, 3, 3 };
    //ハンマー
    private readonly int[] hammer = { -1, -1, 0, 0, 0 };
    //メテオ
    private readonly int[] meteo = { 2, 2, 2, 2, 2 };

    //入力された素材(配列)
    private int matlCount = 0;
    private int[] inputMatl = new int[5];
    private int changeFlag = 0;
    private bool changeFin = false;

    //リセット用
    private bool resetFlag = false;
    public bool ResetFlag
    {
        get { return resetFlag; }
        set { resetFlag = value; }
    }

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject matlBoxes;
    private GameObject[] matlBox = new GameObject[4];

    //合成成功した際の送り先
    [SerializeField]
    private GameObject synthesisCrystal;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < matlBox.Length; i++)
        {
            matlBox[i] = matlBoxes.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //入力された素材クリスタルの取得

        matlCount = this.transform.childCount;
        
        for (int i = 0; i < matlCount; i++)
        {
            matlCrystals[i] = transform.GetChild(i).gameObject;
            inputMatl[i] = (int)matlCrystals[i].GetComponent<MatlInfo>().matlList;
        }

        if (resetFlag == true)
        {
            MatlManager playerMatlManager = player.GetComponent<MatlManager>();
            for (int i = 0; i < inputMatl.Length; i++)
            {
                if (inputMatl[i] == 0)
                {
                    playerMatlManager.NowMatl[0]++;
                    matlBox[0].GetComponent<MatlInfo>().matlList = MatlInfo.MatlList.stick;
                }
                else if (inputMatl[i] == 1)
                {
                    playerMatlManager.NowMatl[1]++;
                    matlBox[1].GetComponent<MatlInfo>().matlList = MatlInfo.MatlList.triangle;
                }
                else if (inputMatl[i] == 2)
                {
                    playerMatlManager.NowMatl[2]++;
                    matlBox[2].GetComponent<MatlInfo>().matlList = MatlInfo.MatlList.lessThan;
                }
                else if (inputMatl[i] == 3)
                {
                    playerMatlManager.NowMatl[3]++;
                    matlBox[3].GetComponent<MatlInfo>().matlList = MatlInfo.MatlList.circle;
                }
                else
                {
                    return;
                }
                matlCrystals[i].GetComponent<MatlInfo>().matlList = MatlInfo.MatlList.empty;
                resetFlag = false;
            }
        }

        if (!startSynthesis) { return; }
        
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
        WeaponManager playerWeaponManager = player.GetComponent<WeaponManager>();
        WeaponInfo synthesisWeaponInfo = synthesisCrystal.GetComponent<WeaponInfo>();
        //レシピNo.1
        if (a == spear[0] && b == spear[1] &&
            c == spear[2] && d == spear[3] && e == spear[4])
        {
            if (playerWeaponManager.NowWeapon[0] >= 5)
            {   
                Debug.Log("所持上限を超えています");
                return;
            }
            Debug.Log("槍");
            synthesisWeaponInfo.weaponList = WeaponInfo.WeaponList.spear;
        }
        else if (a == ax[0] && b == ax[1] && c == ax[2] && d == ax[3] && e == ax[4])
        {
            if (playerWeaponManager.NowWeapon[1] >= 5)
            {
                Debug.Log("所持上限を超えています");
                return;
            }
            Debug.Log("斧");
            synthesisWeaponInfo.weaponList = WeaponInfo.WeaponList.ax;
        }
        else if (a == shield[0] && b == shield[1] && c == shield[2] && d == shield[3] && e == shield[4])
        {
            if (playerWeaponManager.NowWeapon[2] >= 5)
            {   
                Debug.Log("所持上限を超えています");
                return;
            }
            Debug.Log("盾");
            synthesisWeaponInfo.weaponList = WeaponInfo.WeaponList.shield;
        }
        else if (a == twinSword[0] && b == twinSword[1] && c == twinSword[2] && d == twinSword[3] && e == twinSword[4])
        {
            if (playerWeaponManager.NowWeapon[3] >= 5)
            {
                Debug.Log("所持上限を超えています");
                return;
            }
            Debug.Log("双剣");
            synthesisWeaponInfo.weaponList = WeaponInfo.WeaponList.twinSword;
        }
        else if (a == cymbal[0] && b == cymbal[1] && c == cymbal[2] && d == cymbal[3] && e == cymbal[4])
        {
            if (playerWeaponManager.NowWeapon[4] >= 5)
            {
                Debug.Log("所持上限を超えています");
                return;
            }
            Debug.Log("シンバル");
            synthesisWeaponInfo.weaponList = WeaponInfo.WeaponList.cymbal;
        }
        else if (a == hammer[0] && b == hammer[1] && c == hammer[2] && d == hammer[3] && e == hammer[4])
        {
            if (playerWeaponManager.NowWeapon[5] >= 5)
            {
                Debug.Log("所持上限を超えています");
                return;
            }
            Debug.Log("ハンマー");
            synthesisWeaponInfo.weaponList = WeaponInfo.WeaponList.hammer;
        }
        else if (a == meteo[0] && b == meteo[1] && c == meteo[2] && d == meteo[3] && e == meteo[4])
        {
            if (playerWeaponManager.NowWeapon[6] >= 5)
            {
                Debug.Log("所持上限を超えています");
                return;
            }
            Debug.Log("メテオ");
            synthesisWeaponInfo.weaponList = WeaponInfo.WeaponList.meteor;
        }
        //レシピNo.0は失敗
        else
        {
            Debug.Log("失敗");
            endFlag = false;
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
    }
}
 