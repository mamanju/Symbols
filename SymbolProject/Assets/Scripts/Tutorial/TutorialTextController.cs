using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialTextController : MonoBehaviour
{
    private Text tutorialText;

    private string[] sentence = new string[10];
    private int sentenceNum;
    public int SentenceNum
    {
        set { sentenceNum = value; }
    }
    private int textNum;

    private float speed = 0.05f;
    private float text_speed;

    private bool startCount;
    private bool endDisplay;
    public bool EndDisplay
    {
        get { return endDisplay; }
    }

    private int lastSentence;

    public static TutorialTextController instance;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Tutorial")
        {
            transform.parent.gameObject.SetActive(false);
        }

        tutorialText = GetComponent<Text>();
        SetSentence();
        text_speed = speed;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSentence != sentenceNum && endDisplay == true)
        {
            textNum = 0;
            endDisplay = false;
        }

        if (sentence[sentenceNum].Length != textNum - 1 && endDisplay == false)
        {
            startCount = true;
        }
        else
        {
            startCount = false;
            endDisplay = true;
            lastSentence = sentenceNum;
        }

        if (startCount == false) { return; }

        text_speed -= Time.deltaTime;

        if (text_speed <= 0)
        {
            DisplaySentence();
            text_speed = speed;
            textNum++;
        }
    }

    private void SetSentence()
    {
        sentence[0] = "これから操作説明を始めるよ！";
        sentence[1] = "右スティックでカメラが動かせるよ！\n押し込むとカメラがリセットされるよ！";
        sentence[2] = "×ボタンでジャンプをするよ！";
        sentence[3] = "左スティックで移動ができるよ！\n押し込みでダッシュが出来るよ！";
        sentence[4] = "これは合成用クリスタルだよ！\n触れると拾えるよ！";
        sentence[5] = "これはこれは...スライム君ですね。\nやっちゃいましょ。\n○ボタンで攻撃できるよ！";
        sentence[6] = "スライムの色と同じ種類の\nクリスタルがゲットできるよ！";
        sentence[7] = "剣だけだじゃ飽きちゃうよね。\n新しい武器を作ろう！\n△ボタンで合成画面が開くよ！";
        sentence[8] = "十字キーでクリスタルを選んで\n○ボタンで決定だよ！\n合成できる組み合わせだと表示が変わるよ";
    }

    private void DisplaySentence()
    {
        GetComponent<Text>().text = sentence[sentenceNum].Substring(0, textNum);
    }
}
