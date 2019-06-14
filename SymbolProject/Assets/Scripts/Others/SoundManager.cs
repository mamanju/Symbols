using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] sfx;
    public static SoundManager instance;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        stopSfx();
        playSfx(0);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Z))
        {
            stopSfx();
            playSfx(0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            stopSfx();
            playSfx(1);
        }
    }

    public void playSfx(int number)
    {
        sfx[number].Play();
    }

    public void stopSfx()
    {
        for(int i = 0; i < sfx.Length; i++)
        {
            sfx[i].Stop();
        }
    }
}
