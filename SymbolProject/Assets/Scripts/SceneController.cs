using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SingletonMonoBehaviour<SceneController>
{
    /// <summary>
    /// シーン名
    /// </summary>
    public enum SceneName
    {
        Title,
        StageSelect,
        StageFirst,
        BossScene,
    }

    public SceneName names;
    // 遷移の時間
    [SerializeField,Header("フェードの時間")]
    private float fadeTime;

    /// <summary>
    /// シングルトン
    /// </summary>
    void Awake() {
        if(this != Instance) {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// シーン遷移
    /// </summary>
    /// <param name="sceneName">シーン名</param>
    public void ChangeScene(string sceneName) {
        FadeManager.Instance.LoadScene(sceneName, fadeTime);
    }

}
