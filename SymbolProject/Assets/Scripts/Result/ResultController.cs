using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルトコントローラー
/// </summary>
public class ResultController : MonoBehaviour
{
   public void ChangeSceneSelect(string name) {
        SceneController.Instance.ChangeScene(name);
   }
}
