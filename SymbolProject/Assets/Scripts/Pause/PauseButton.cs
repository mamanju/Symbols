using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField]
    [Range (0,1)]private int selectNum;
    
    public int GetSelectNum
    {
        get { return selectNum; }
    }

    public void Decision()
    {
        SceneController.Instance.ChangeScene(SceneController.SceneName.Title);
    }
}
