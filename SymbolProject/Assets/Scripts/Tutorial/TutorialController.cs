using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private TutorialTextController tutorialTextController;

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
            Debug.Log("呼んだ？");
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
        }
        TextChange();

        if (tutorial_Flag[3] == false) { return; }
        TextChange();

        //クリスタル取得
        if (tutorial_Flag[4] == false) { return; }
        TextChange();

        //敵を倒す
        if (tutorial_Flag[5] == false) { return; }
        TextChange();

        //クリスタル取得
        if (tutorial_Flag[6] == false) { return; }
        TextChange();

        //合成画面を開く
        if (tutorial_Flag[7] == false) { return; }
        TextChange();

        //合成
        if (tutorial_Flag[8] == false) { return; }
        TextChange();

        //合成画面を閉じる
        if (tutorial_Flag[9] == false) { return; }
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
