using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialTextController : MonoBehaviour
{
    private Text tutorialText;

    private string[] sentence = new string[14];
    private int sentenceNum;
    public int SentenceNum
    {
        set { sentenceNum = value; }
    }
    private int textNum;
    public int TextNum
    {
        set { textNum = value; }
    }

    [SerializeField]
    private float text_time = 0.1f;
    private float text_speed;

    [SerializeField]
    private float load_time = 0.5f;
    private float loadd_speed;

    private bool load_flag;

    private bool startCount = true;
    public bool StartCount
    {
        set { startCount = value; }
    }
    private bool endDisplay;
    public bool EndDisplay
    {
        get { return endDisplay; }
    }

    private bool[] tutorial_Flag = new bool[13];
    public bool[] GetTutorial_Flag
    {
        get { return tutorial_Flag; }
    }

    private int lastSentence;
    
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Tutorial")
        {
            transform.parent.gameObject.SetActive(false);
        }

        tutorialText = GetComponent<Text>();
        SetSentence();
        text_speed = text_time;
        loadd_speed = load_time;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSentence != sentenceNum && endDisplay == true)
        {
            endDisplay = false;
        }
        
        if (sentence[sentenceNum].Length == textNum - 1)
        {
            startCount = false;
            load_flag = true;
            lastSentence = sentenceNum;
        }

        if (load_flag == true)
        {
            loadd_speed -= Time.fixedDeltaTime;

            if (loadd_speed <= 0)
            {
                load_flag = false;
                loadd_speed = load_time;
                endDisplay = true;
                tutorial_Flag[sentenceNum] = true;
                if (sentenceNum != 0)
                {
                    tutorial_Flag[sentenceNum - 1] = false;
                }
            }
        }

        if (startCount == false) { return; }

        //一文字ずつの時間
        text_speed -= Time.fixedDeltaTime;

        if (text_speed <= 0)
        {
            DisplaySentence();
            text_speed = text_time;
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
        sentence[5] = "この可愛いスライムは敵だよ！\n○ボタンで攻撃できるよ！";
        sentence[6] = "スライムの色によってゲットできる\nクリスタルが変わるよ！";
        sentence[7] = "クリスタルを使って\n剣以外の武器を作ってみよう！\n△ボタンで合成画面が開くよ！";
        sentence[8] = "十字キーでクリスタルを選んで\n○ボタンで決定だよ！";
        sentence[9] = "合成できる組み合わせだと\n表示が変わるよ!\n□ボタンで合成できるよ！";
        sentence[10] = "×ボタンで合成画面が閉じるよ";
        sentence[11] = "R1、L1で武器を切り替えられるよ\n武器にはそれぞれ個性があるよ！";
        sentence[12] = "OPTIONSで操作を確認できるよ！";
        sentence[13] = "チュートリアルを終了するよ！";
    }
    private void DisplaySentence()
    {
        GetComponent<Text>().text = sentence[sentenceNum].Substring(0, textNum);
    }
}
