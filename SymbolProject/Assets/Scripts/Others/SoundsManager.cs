using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundsManager : MonoBehaviour
{
    //0.Title、1.Map、2.Boss、3.GameOver、4.GameClear
    [SerializeField]
    private AudioClip[] clips_BGM;
    //0.Sword、1.Spear、2.Ax、3.Damage、4.Jump
    [SerializeField]
    private AudioClip[] clips_SE_player;
    //0.Damage(slime)、1.Destroy(slime)、2.Attack(slime)、
    //3.Damage(boss)、4.Destroy(boss)、5.Attack(boss)
    [SerializeField]
    private AudioClip[] clips_SE_enemy;
    //0.炎、1.
    [SerializeField]
    private AudioClip[] clips_SE_gimmick;

    private AudioSource bgmSource;
    private AudioSource[] seSources;

    [SerializeField]
    private float stop_speed = 1;
    private bool stop_flag;

    public static SoundsManager instance;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        bgmSource = GetComponent<AudioSource>();
        seSources = GetComponents<AudioSource>();

        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "title")
        {
            SetBGM(0);
        }
        else if (sceneName == "StageFirst")
        {
            SetBGM(1);
        }
        else if (sceneName == "BossStage")
        {
            SetBGM(2);
        }
        else
        {
            SetBGM(0);
        }
        PlayBGM();
    }

    // Update is called once per frame
    void Update()
    {
        if (stop_flag == true)
        {
            bgmSource.volume -= stop_speed * Time.deltaTime;
        }
        if (bgmSource.volume <= 0)
        {
            bgmSource.Stop();
        }
    }

    //現在のBGMをセットする
    public void SetBGM(int _num)
    {
        bgmSource.clip = clips_BGM[_num];
    }

    //BGMを流す
    public void PlayBGM()
    {
        bgmSource.volume = 1;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        stop_flag = true;
    }

    public void PlaySE_player(int _num)
    {
        seSources[0].PlayOneShot(clips_SE_player[_num]);
    }

    public void PlaySE_enemy(int _num)
    {
        seSources[0].PlayOneShot(clips_SE_enemy[_num]);
    }

    public void PlaySE_gimmick(int _num)
    {
        seSources[0].PlayOneShot(clips_SE_gimmick[_num]);
    }

    public void StopSE(int _num)
    {
        seSources[_num].Stop();
    }
}
