using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//スイッチで変わる炎のギミックのEffectのスクリプト
public class StaticTrapEffect : MonoBehaviour
{
    public static StaticTrapEffect instance;
    public GameObject TrapEffect;
    public float sec;
    void Start()
    {
        instance = this;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TrapEffect.gameObject.SetActive(false);
            StaticTrap.instance.FireOff();
        }
    }
}