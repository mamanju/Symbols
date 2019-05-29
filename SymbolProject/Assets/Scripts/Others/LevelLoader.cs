using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//////////sceneをロードするため
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    //プレーヤーがエリアに入るflag
    private bool PlayerInZone;
    //ロードするステージの選択
    public string LevelToLoad;
    //levelUnlock　のため
    public string LevelTag;


    // Use this for initialization
    void Start() {
        //false状態で
        PlayerInZone = false;}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow) && PlayerInZone)
        {

            LoadLevel();
           
        }
    }
    public void LoadLevel()
    {
        PlayerPrefs.SetInt(LevelTag, 1);
        SceneManager.LoadSceneAsync(LevelToLoad);
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        //colliderのTriggerzoneに入る場合、PlayerInZoneはtrueになる。
        if (collision.name == "Player")
        {
            PlayerInZone = true;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //colliderのTriggerzoneから出る場合、PlayerInZoneはfalseになる。
        if (collision.name == "Player")
        {
            PlayerInZone = false;
        }
    }
        
        }
    
