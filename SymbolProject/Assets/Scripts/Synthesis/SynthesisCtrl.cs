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

    public void Synthesis(int _a, int _b, int _c, int _d, int _e)
    {
        WeaponManager playerWeaponManager = player.GetComponent<WeaponManager>();
        WeaponInfo synthesisWeaponInfo = synthesisCrystal.GetComponent<WeaponInfo>();
        //レシピNo.1
        if (_a == spear[0] && _b == spear[1] &&
            _c == spear[2] && _d == spear[3] && _e == spear[4])
        {
            if (playerWeaponManager.NowWeapon[0] >= 5)
            {   
                Debug.Log("所持上限を超えています");
                return;
            }
            Debug.Log("槍");
            synthesisWeaponInfo.weaponList = WeaponInfo.WeaponList.spear;
        }
        else if (_a == ax[0] && _b == ax[1] && _c == ax[2] && _d == ax[3] && _e == ax[4])
        {
            if (playerWeaponManager.NowWeapon[1] >= 5)
            {
                Debug.Log("所持上限を超えています");
                return;
            }
            Debug.Log("斧");
            synthesisWeaponInfo.weaponList = WeaponInfo.WeaponList.ax;
        }
        else if (_a == shield[0] && _b == shield[1] && _c == shield[2] && _d == shield[3] && _e == shield[4])
        {
            if (playerWeaponManager.NowWeapon[2] >= 5)
            {   
                Debug.Log("所持上限を超えています");
                return;
            }
            Debug.Log("盾");
            synthesisWeaponInfo.weaponList = WeaponInfo.WeaponList.shield;
        }
        else if (_a == twinSword[0] && _b == twinSword[1] && _c == twinSword[2] && _d == twinSword[3] && _e == twinSword[4])
        {
            if (playerWeaponManager.NowWeapon[3] >= 5)
            {
                Debug.Log("所持上限を超えています");
                return;
            }
            Debug.Log("双剣");
            synthesisWeaponInfo.weaponList = WeaponInfo.WeaponList.twinSword;
        }
        else if (_a == cymbal[0] && _b == cymbal[1] && _c == cymbal[2] && _d == cymbal[3] && _e == cymbal[4])
        {
            if (playerWeaponManager.NowWeapon[4] >= 5)
            {
                Debug.Log("所持上限を超えています");
                return;
            }
            Debug.Log("シンバル");
            synthesisWeaponInfo.weaponList = WeaponInfo.WeaponList.cymbal;
        }
        else if (_a == hammer[0] && _b == hammer[1] && _c == hammer[2] && _d == hammer[3] && _e == hammer[4])
        {
            if (playerWeaponManager.NowWeapon[5] >= 5)
            {
                Debug.Log("所持上限を超えています");
                return;
            }
            Debug.Log("ハンマー");
            synthesisWeaponInfo.weaponList = WeaponInfo.WeaponList.hammer;
        }
        else if (_a == meteo[0] && _b == meteo[1] && _c == meteo[2] && _d == meteo[3] && _e == meteo[4])
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
 