using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM_SoundManager : MonoBehaviour
{
    [Header("0.GameOver,1.GameClear")]
    [SerializeField]
    private AudioClip[] clips_BGM;
    
    private AudioSource bgmSource;
    [SerializeField]
    private float stop_speed = 1;
    private bool stop_flag;

    // Start is called before the first frame update
    void Start()
    {
        bgmSource = GetComponent<AudioSource>();
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
    //BGMを変える
    public void ChangeBGM(int _num)
    {
        bgmSource.clip = clips_BGM[_num];
        PlayBGM();
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

    //BGMを止める
    public void StopBGM()
    {
        stop_flag = true;
    }
}
