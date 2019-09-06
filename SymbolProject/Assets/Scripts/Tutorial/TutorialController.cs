using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private TutorialTextController tutorialTextController;
    [SerializeField]
    private GameObject crystal;
    [SerializeField]
    private GameObject slime_enemy;

    private bool crystalGet;
    public bool CrystalGet
    {
        get { return crystalGet; }
        set { crystalGet = value; }
    }

    private bool enemyDown;
    public bool EnemyDown
    {
        set { enemyDown = value; }
    }

    private bool crystalGet_2;
    public bool CrystalGet_2
    {
        get { return crystalGet_2; }
        set { crystalGet_2 = value; }
    }

    private bool SetCeystal;
    private bool SetEnemy;
    
    private bool[] passFlag = new bool[12];
    private bool[] tutorial_Flag = new bool[14];

    public static TutorialController instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        crystal.SetActive(false);
        slime_enemy.SetActive(false);
        tutorial_Flag = tutorialTextController.GetTutorial_Flag;
    }

    // Update is called once per frame
    void Update()
    {

        //カメラ移動
        TextChange(0);
        if (Input.GetAxis("Horizontal_R") != 0)
        {
            passFlag[0] = true;
        }
        if (!passFlag[0]) { return; }

        //ジャンプ
        TextChange(1);
        if (Input.GetButtonDown("Cross"))
        {
            passFlag[1] = true;
        }
        if (!passFlag[1]) { return; }

        //移動
        TextChange(2);
        if (Input.GetAxis("Horizontal_L") != 0)
        {
            passFlag[2] = true;
        }
        if (!passFlag[2]) { return; }

        //クリスタル取得
        TextChange(3);
        if (SetCeystal == false)
        {
            SetCeystal = true;
            crystal.SetActive(true);
        }
        if (!crystalGet) { return; }
        
        //敵を倒す
        TextChange(4);
        if (SetEnemy == false)
        {
            SetEnemy = true;
            slime_enemy.SetActive(true);
        }
        if (!enemyDown) { return; }

        //クリスタル取得
        TextChange(5);
        if (!crystalGet_2) { return; }

        //合成画面を開く
        TextChange(6);
        if (Input.GetButtonDown("Triangle"))
        {
            passFlag[4] = true;
        }
        if (!passFlag[4]) { return; }

        //クリスタル選択
        TextChange(7);
        if (Input.GetButtonDown("Circle"))
        {
            passFlag[5] = true;
        }
        if (!passFlag[5]) { return; }

        //合成
        TextChange(8);
        if (Input.GetButtonDown("Square"))
        {
            passFlag[6] = true;
        }
        if (!passFlag[6]) { return; }

        //合成画面を閉じる
        TextChange(9);
        if (Input.GetButtonDown("Cross"))
        {
            passFlag[7] = true;
        }
        if (!passFlag[7]) { return; }

        //武器を切り替える
        TextChange(10);
        if (Input.GetButtonDown("R1") || Input.GetButtonDown("L1"))
        {
            passFlag[8] = true;
        }
        if (!passFlag[8]) { return; }

        TextChange(11);

        TextChange(12);

        //Scene切り替え
        if (tutorial_Flag[13] == true)
        {
            SceneController.Instance.ChangeScene(SceneController.SceneName.StageFirst);
        }
    }

    public void TextChange(int _num)
    {
        if (EndSentence() == false) { return; }
        if (tutorial_Flag[_num] == true)
        {
            tutorialTextController.SentenceNum = _num + 1;
            tutorialTextController.StartCount = true;
            tutorialTextController.TextNum = 0;
        }
    }

    public bool EndSentence()
    {
        return tutorialTextController.EndDisplay;
    }
}
