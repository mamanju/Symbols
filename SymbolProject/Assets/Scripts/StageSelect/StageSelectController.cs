using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージセレクトコントローラー
/// </summary>
public class StageSelectController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] selectPanels;

    private int panelNum;

    private void Update()
    {
        if (Input.GetAxisRaw("CrossKey_H") > 0)
        {
            panelNum++;
            if(panelNum >= selectPanels.Length)
            {
                panelNum = 0;
            }
        }else if(Input.GetAxisRaw("CrossKey_H") < 0)
        {
            panelNum--;
            if (panelNum < 0)
            {
                panelNum = selectPanels.Length - 1;
            }
        }


        selectPanels[panelNum].transform.localScale = new Vector2(1.2f, 1.2f);
    }

    public void ChangeGame() {
        SceneController.Instance.ChangeScene(SceneController.SceneName.StageFirst);
    }
}
