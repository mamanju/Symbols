using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP_Slider : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyController;
    private int bossHP;
    private int bossHP_Max;

    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        bossHP = enemyController.Health;
        bossHP_Max = enemyController.Health;
        slider.maxValue = bossHP_Max;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = enemyController.Health;
    }
}
