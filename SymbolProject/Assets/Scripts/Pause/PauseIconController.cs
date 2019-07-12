using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseIconController : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseCon;
    [SerializeField]
    private float moveSpeed;
    private float Horizontal;
    private float Vertical;
    private GameObject targetObj;
    private Vector3 cursorPosition;

    private void OnEnable()
    {
        targetObj = null;
        cursorPosition = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        transform.position = cursorPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal_L");
        Vertical = Input.GetAxis("Vertical_L");

        // ポーズ内のボタンを押したときの処理
        if (Input.GetButtonDown("Circle") && targetObj != null)
        {
            PauseButton pButton = targetObj.GetComponent<PauseButton>();
            if (pButton.GetSelectNum == 0)
            {
                pButton.Decision();
            }
            else
            {
                pauseCon.GetComponent<PauseController>().Pause();
            }
        }

        // 横移動
        if (Horizontal != 0)
        {
            cursorPosition.x += Horizontal * moveSpeed;
        }

        // 縦移動
        if (Vertical != 0)
        {
            cursorPosition.y += Vertical * moveSpeed;
        }

        // 可動制限----------------------------------------------------------------
        if (cursorPosition.x > Screen.width || cursorPosition.x < 0)
        {
            cursorPosition = transform.position;
        }

        if (cursorPosition.y > Screen.height || cursorPosition.y < 0)
        {
            cursorPosition = transform.position;
        }
        // ------------------------------------------------------------------------
        transform.position = cursorPosition;
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        targetObj = col.gameObject;
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        targetObj = null;
    }
}
