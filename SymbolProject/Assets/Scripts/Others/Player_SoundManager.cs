using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SoundManager : MonoBehaviour
{
    [Header("0.Sword,1.Spear,2.Ax,3.Damage,4.Jump")]
    [SerializeField]
    private AudioClip[] clips_SE_player;

    private AudioSource seSources;

    public static Player_SoundManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        seSources = GetComponent<AudioSource>();
    }

    public void PlaySE_player(int _num)
    {
        seSources.clip = clips_SE_player[_num];
        seSources.Play();
    }
}
