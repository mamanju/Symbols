using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextController : MonoBehaviour
{
    //スタートのあいさつ
    private bool start_Flag;

    //カメラ移動
    private bool camera_Flag;
    //ジャンプ
    private bool jump_Flag;
    //移動
    private bool move_Flag;
    //クリスタル取得
    private bool crystal_Flag_1;
    //敵を倒す
    private bool enemy_Flag;
    //クリスタル取得
    private bool crystal_Flag_2;
    //合成画面を開く
    private bool synthesis_Flag_open;
    //合成
    private bool synthesis_Flag;
    //合成画面を閉じる
    private bool synthesis_Flag_close;

    private Text tutorialText;

    private string[] sentence = new string[10];
    private int sentenceNum;
    private int textNum;

    private float speed = 0.05f;
    private float text_speed;

    private bool startCount;
    private bool endDisplay;

    // Start is called before the first frame update
    void Start()
    {
        tutorialText = GetComponent<Text>();
        SetSentence();
        text_speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        tutorialText.text = sentence[7];

        if (sentence[sentenceNum].Length != textNum - 1 && endDisplay == false)
        {
            startCount = true;
        }
        else
        {
            startCount = false;
            endDisplay = true;
        }

        if(startCount == false) { return; }

        text_speed -= Time.deltaTime;

        if (text_speed <= 0)
        {
            DisplaySentence();
            text_speed = speed;
            textNum++;
        }

        if (start_Flag == false) { return; }

        if (camera_Flag == false) { return; }

        if (jump_Flag == false) { return; }

        if (move_Flag == false) { return; }

        if (crystal_Flag_1 == false) { return; }

        if (enemy_Flag == false) { return; }

        if (crystal_Flag_2 == false) { return; }

        if (synthesis_Flag_open == false) { return; }

        if (synthesis_Flag == false) { return; }

        if (synthesis_Flag_close == false) { return; }
    }

    private void SetSentence()
    {
        sentence[0] = "これから操作説明を始めるよ！";
        sentence[1] = "右スティックでカメラが動かせるよ！\n押し込むとカメラがリセットされるよ！";
        sentence[2] = "×ボタンでジャンプをするよ！";
        sentence[3] = "左スティックで移動ができるよ！\n押し込みでダッシュが出来るよ！";
        sentence[4] = "これは合成用クリスタルだよ！\n触れると拾えるよ！";
        sentence[5] = "これはこれは...スライム君ですね。\nやっちゃいましょ。\n○ボタンで攻撃できるよ！";
        sentence[6] = "剣だけだじゃ飽きちゃうよね。\n新しい武器を作ろう！\n□ボタンで合成画面が開くよ！";
        sentence[7] = "十字キーでクリスタルを選んで\n○ボタンで決定だよ！\n合成できる組み合わせだと表示が変わるよ";
    }

    private void DisplaySentence()
    {
        GetComponent<Text>().text = sentence[sentenceNum].Substring(0, textNum);
    }
}
