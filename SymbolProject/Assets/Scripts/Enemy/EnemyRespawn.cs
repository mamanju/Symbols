using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    private EnemyManager eManager;

    private bool isSpawn = false;

    private string monsterPath = "Prefabs/Enemies/slime_";

    private float timer;

    //[SerializeField]
    private float spawmTime = 10;

    public bool setIsSpawn {
        set { isSpawn = value; }
    }

    void Start() {
        isSpawn = false;
        eManager = transform.parent.GetComponent<EnemyManager>();
        transform.SetParent(transform.parent.transform.parent);
    }

    void Update() {
        if (!isSpawn) {
            return;
        }
        timer += Time.deltaTime;
        if (timer >= spawmTime) {
            ReSpawn();
        }
    }

    public void ReSpawn() {
        Debug.Log("call");
        string path = "";
        switch (eManager.crystal) {
            case EnemyManager.Crystals.Circle: // Blue
                path = monsterPath + "B";
                break;
            case EnemyManager.Crystals.LessThan: // Yellow
                path = monsterPath + "Y";
                break;
            case EnemyManager.Crystals.Stick: // Green
                path = monsterPath + "G";
                break;
            case EnemyManager.Crystals.Triangle: // Red
                path = monsterPath + "R";
                break;
            default:
                break;
        }
        GameObject monster = Resources.Load(path) as GameObject;
        Instantiate(monster);
        monster.transform.position = transform.position;
        Destroy(gameObject);
    }
}
