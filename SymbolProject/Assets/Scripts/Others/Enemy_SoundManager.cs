using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SoundManager : MonoBehaviour
{
    [Header("boss-3.Damage,4.Destroy,5.Attack")]
    [Header("slime-0.Damage,1.Destroy,2.Attack")]
    [SerializeField]
    private AudioClip[] clips_SE_enemy;

    private AudioSource seSources;

    public static Enemy_SoundManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        seSources = GetComponent<AudioSource>();
    }

    public void PlaySE_enemy(int _num)
    {
        seSources.clip = clips_SE_enemy[_num];
        seSources.Play();
    }
}
