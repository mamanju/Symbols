using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
        StageSecond,
        BossStage,
        Result,
    }

    private bool isFading = false;

    // 遷移の時間
    [SerializeField,Header("フェードの時間")]
    private float fadeTime;

    private float fadeAlpha;

    private Color fadeColor = Color.black; 

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
        
    public void OnGUI()
    {
        if (this.isFading)
        {
            this.fadeColor.a = this.fadeAlpha;
            GUI.color = this.fadeColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }
    }

    /// <summary>
    /// フェード処理
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IEnumerator Fade(SceneName name)
    {
        this.isFading = true;
        float time = 0;

        while (time <= fadeTime)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / fadeTime);
            time += Time.deltaTime;
            yield return 0;
        }

        SceneManager.LoadScene((int)name);

        time = 0;
        while (time <= fadeTime)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / fadeTime);
            time += Time.deltaTime;
            yield return 0;
        }
        this.isFading = false;
    }


    /// <summary>
    /// シーン遷移
    /// </summary>
    /// <param name="sceneName">シーン名</param>
    public void ChangeScene(SceneName name)
    {
        StartCoroutine(Fade(name));
        var eventSystem = FindObjectOfType<EventSystem>();
        eventSystem.enabled = false;
    }

}
