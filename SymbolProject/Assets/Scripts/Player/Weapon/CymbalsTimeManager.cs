using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CymbalsTimeManager : MonoBehaviour
{
    [SerializeField]
    private WeaponAtaccks weaponAtaccks;
    
    private float stunTime = 5;

    private List<GameObject> enemyList = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if (weaponAtaccks.StunFlag == true)
        {
            stunTime -= Time.unscaledDeltaTime;
            EnemyStun();
        }
        if (stunTime <= 0)
        {
            weaponAtaccks.StunFlag = false;
            stunTime = 5;
            EnemyReset();
        }
    }

    public void AddEnemy(GameObject _enemy)
    {
        Debug.Log(_enemy.name);
        enemyList.Add(_enemy);
    }

    private void EnemyStun()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].GetComponent<EnemyAI>().currentState = EnemyAI.AIState.isIdle;
        }
    }

    private void EnemyReset()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            weaponAtaccks.CymbalsEnd(enemyList[i]);
            enemyList.Remove(enemyList[i]);
        }
    }
}
