using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//時間でActiveとDesactiveに変わる炎のギミックの時間のスクリプト
public class VariableTrapTimer : MonoBehaviour
{
    private VariableTrapTimer vTrapTimer;
    private VariableTrap vTrap;

    private bool fireFlag = false;

    public GameObject FireEffect;
    private float sec;
    private bool active = false;
    void Start()
    {
        vTrap = GetComponent<VariableTrap>();
        vTrapTimer = this;
        StartCoroutine(FireRound());
    }
    private void Update()
    {
        if (!vTrap.ActiveFlag) {
            if (!active) {
                StartCoroutine(FireRound());
            }
        }
    }

    IEnumerator FireRound()
    {
        active = true;
        sec = Random.Range(0.5f,2);
        FireEffect.gameObject.SetActive(true);
        vTrap.FireOff();
        yield return new WaitForSeconds(sec);

        FireEffect.gameObject.SetActive(false);
        vTrap.FireOn();
        yield return new WaitForSeconds(sec);

        active = false;
    }
}