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
    private Image[] SelectButtons;

    private int buttonNum = 0;

    private bool pauseFlag = false;
    private bool gameOverFlag = false;
    private bool selectFlag = false;
    // Start is called before the first frame update
    void Awake()
    {
        pauseUI.SetActive(false);
        gameOverUI.SetActive(false);
    }

    private void Update() {
        if (Input.GetButtonDown("Option")) {
            Pause();
        }

        if (!pauseFlag) {
            return;
        }

        if (Input.GetButtonDown("Circle")) {
            if(buttonNum == 0) {
                SceneController.Instance.ChangeScene("Title");
            } else {
                Pause();
            }
        }

        Debug.Log(Input.GetAxis("CrossKey_V"));
        if(Input.GetAxis("CrossKey_V") == 0) {
            selectFlag = false;
            return;
        }

        if (selectFlag) {
            return;
        }

        if(Input.GetAxis("CrossKey_V") < 0) {
            if(buttonNum - 1 < 0) {
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
        if(buttonNum == 0) {
            SelectButtons[buttonNum].transform.localScale = new Vector2(1.2f, 1.2f);
            SelectButtons[buttonNum + 1].transform.localScale = new Vector2(1f, 1f);
        } else {
            SelectButtons[buttonNum].transform.localScale = new Vector2(1.2f, 1.2f);
            SelectButtons[buttonNum - 1].transform.localScale = new Vector2(1f, 1f);
        }
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
        } else {
            gameOverFlag = false;
            gameOverUI.SetActive(false);
        }
        
    }
}
