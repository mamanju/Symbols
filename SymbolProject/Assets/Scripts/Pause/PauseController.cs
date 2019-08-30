using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲーム停止時の処理(ポーズ、ゲームオーバー)
/// </summary>
public class PauseController : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseUI;

    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private GameObject gameClearUI;

    [SerializeField]
    private Image[] SelectButtons;

    private int buttonNum = 0;

    private bool pauseFlag = false;
    private bool gameOverFlag = false;
    private bool selectFlag = false;
    private bool clearFlag = false;

    public bool ClearFlag {
        get { return clearFlag; }
        set { clearFlag = value; }
    }
    // Start is called before the first frame update
    void Awake()
    {
        pauseUI.SetActive(false);
        gameOverUI.SetActive(false);
        gameClearUI.SetActive(false);
    }

    private void Update() {


        if (clearFlag)
        {
            gameClearUI.SetActive(true);
        }
        if (Input.GetButtonDown("Option"))
        {
            Pause();
        }

        #region 十字キー操作
        if (Input.GetButtonDown("Circle")) {
            if (clearFlag) {
                SceneController.Instance.ChangeScene(SceneController.SceneName.Title);
            }

            if (!pauseFlag) {
                return;
            }
            if (buttonNum == 0) {
                SceneController.Instance.ChangeScene(SceneController.SceneName.Title);
            } else {
                Pause();
            }
        }

        if (!pauseFlag) {
            return;
        }

        Debug.Log(Input.GetAxis("CrossKey_H"));
        if (Input.GetAxis("CrossKey_H") == 0) {
            selectFlag = false;
            return;
        }

        if (selectFlag) {
            return;
        }

        if (gameOverFlag)
        {
            if (Input.GetButtonDown("Circle"))
            {
                SceneController.Instance.ChangeScene(SceneController.SceneName.Title);
            }
        }

        if (Input.GetAxis("CrossKey_H") < 0) {
            if (buttonNum - 1 < 0) {
                buttonNum = 1;
            } else {
                buttonNum--;
            }
        } else {
            if (buttonNum + 1 > 1) {
                buttonNum = 0;
            } else {
                buttonNum++;
            }
        }
        if (buttonNum == 0) {
            SelectButtons[buttonNum].transform.localScale = new Vector2(1.2f, 1.2f);
            SelectButtons[buttonNum + 1].transform.localScale = new Vector2(1f, 1f);
        } else {
            SelectButtons[buttonNum].transform.localScale = new Vector2(1.2f, 1.2f);
            SelectButtons[buttonNum - 1].transform.localScale = new Vector2(1f, 1f);
        }
        #endregion
        selectFlag = true;
    }

    /// <summary>
    /// ポーズ
    /// </summary>
    public void Pause() {
        if (!pauseFlag) {
            pauseFlag = true;
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        } else {
            pauseFlag = false;
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// ゲームオーバー
    /// </summary>
    public void GameOver() {
        if (!gameOverFlag) {
            gameOverFlag = true;
            gameOverUI.SetActive(true);
            Time.timeScale = 0;
        } else {
            gameOverFlag = false;
            gameOverUI.SetActive(false);
        }
        
    }
}
