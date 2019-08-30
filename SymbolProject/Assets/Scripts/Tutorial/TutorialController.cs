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

    private int boolNum;

    private bool[] tutorial_Flag = new bool[10];
    public void Tutorial_Flag(int i)
    {
        tutorial_Flag[i] = true;
    }

    public bool[] GetTutorial_Flag
    {
        get { return tutorial_Flag; }
    }

    public static TutorialController instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        crystal.SetActive(false);
        slime_enemy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Circle") && tutorial_Flag[0] == false && EndSentence() == true)
        {
            tutorial_Flag[0] = true;
        }

        if (tutorial_Flag[0] == false) { return; }
        TextChange();


        //カメラ移動
        if (Input.GetAxis("Horizontal_R") != 0 && tutorial_Flag[1] == false && EndSentence() == true)
        {
            tutorial_Flag[1] = true;
        }
        TextChange();

        if (tutorial_Flag[1] == false) { return; }

        //ジャンプ
        if (Input.GetButtonDown("Cross") && tutorial_Flag[2] == false && EndSentence() == true)
        {
            tutorial_Flag[2] = true;
        }
        TextChange();

        if (tutorial_Flag[2] == false) { return; }

        //移動
        if (Input.GetAxis("Horizontal_L") != 0 && tutorial_Flag[3] == false && EndSentence() == true)
        {
            tutorial_Flag[3] = true;
            crystal.SetActive(true);
        }
        TextChange();

        if (tutorial_Flag[3] == false) { return; }

        //クリスタル取得
        if (crystalGet == true && tutorial_Flag[4] == false && EndSentence() == true)
        {
            tutorial_Flag[4] = true;
            slime_enemy.SetActive(true);
        }
        TextChange();

        if (tutorial_Flag[4] == false) { return; }

        //敵を倒す
        if (enemyDown == true && tutorial_Flag[5] == false && EndSentence() == true)
        {
            tutorial_Flag[5] = true;
        }

        TextChange();

        if (tutorial_Flag[5] == false) { return; }
        
        //クリスタル取得
        if (crystalGet_2 == true && tutorial_Flag[6] == false && EndSentence() == true)
        {
            tutorial_Flag[6] = true;
        }
        TextChange();

        if (tutorial_Flag[6] == false) { return; }
        
        //合成画面を開く
        if (Input.GetButtonDown("Triangle") && tutorial_Flag[7] == false && EndSentence() == true)
        {
            tutorial_Flag[7] = true;
        }
        TextChange();

        if (tutorial_Flag[7] == false) { return; }

        //合成
        if (Input.GetButtonDown("Square") && tutorial_Flag[8] == false && EndSentence() == true)
        {
            tutorial_Flag[8] = true;
        }
        TextChange();

        if (tutorial_Flag[8] == false) { return; }

        //合成画面を閉じる
        if (Input.GetButtonDown("Cross") && tutorial_Flag[9] == false && EndSentence() == true)
        {

        }
        TextChange();

        if (tutorial_Flag[9] == false) { return; }

        //武器を切り替える
        TextChange();
        
        if (tutorial_Flag[10] == false) { return; }

        //チュートリアル終了
        TextChange();


        if (tutorial_Flag[11] == false) { return; }
        TextChange();
    }

    public void TextChange()
    {
        if (tutorial_Flag[boolNum] == true && tutorial_Flag[boolNum + 1] == false)
        {
            tutorialTextController.SentenceNum = boolNum + 1;
            boolNum++;
        }
    }

    private bool EndSentence()
    {
        return TutorialTextController.instance.EndDisplay;
    }
}
