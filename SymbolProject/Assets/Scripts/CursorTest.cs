using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorTest : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Vector3 cursorPosition;

    private GameObject targetObj;

    [SerializeField]
    private GameObject gameManager;

    private void OnEnable()
    {
        targetObj = null;
        cursorPosition = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        transform.position = cursorPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal_L");
        float Vertical = Input.GetAxis("Vertical_L");

        if (Input.GetButtonDown("Circle") && targetObj != null)
        {

            switch (gameManager.GetComponent<GameStateController>().PState)
            {
                case GameStateController.PlayerState.Action:
                    break;
                case GameStateController.PlayerState.Pause:
                    //targetObj.GetComponent<>()
                    break;
                case GameStateController.PlayerState.Synthesis:
                    break;
                default:
                    break;
            }
        }

        if(Horizontal > 0)
        {
            cursorPosition.x += moveSpeed;
        }else if(Horizontal < 0)
        {
            cursorPosition.x -= moveSpeed;
        }

        if(Vertical > 0)
        {
            cursorPosition.y += moveSpeed;
        }else if(Vertical < 0)
        {
            cursorPosition.y -= moveSpeed;
        }

        if (cursorPosition.x > Screen.width || cursorPosition.x < 0)
        {
            cursorPosition = transform.position;
        }

        if (cursorPosition.y > Screen.height || cursorPosition.y < 0)
        {
            cursorPosition = transform.position;
        }
        transform.position = cursorPosition;
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        targetObj = col.gameObject;
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("call");
        targetObj = null;
    }
}
