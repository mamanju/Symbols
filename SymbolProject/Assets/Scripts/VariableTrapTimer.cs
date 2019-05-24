using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//時間でActiveとDesactiveに変わる炎のギミックの時間のスクリプト
public class VariableTrapTimer : MonoBehaviour
{
    public static VariableTrapTimer instance;
    public GameObject FireEffect;
    public float sec;
    private bool active = false;
    void Start()
    {
        instance = this;
        StartCoroutine(FireRound());
    }
    private void Update()
    {
        if (!active)
        {
            StartCoroutine(FireRound());
        }
    }

    IEnumerator FireRound()
    {
        active = true;

        FireEffect.gameObject.SetActive(false);
        VariableTrap.instance.FireOff();
        yield return new WaitForSeconds(sec);

        FireEffect.gameObject.SetActive(true);
        VariableTrap.instance.FireOn();
        yield return new WaitForSeconds(sec);

        active = false;
    }
}