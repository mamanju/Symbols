using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearInfo : WeaponCtrl
{
    [SerializeField]
    private GameObject ShotSpear;

    [SerializeField]
    private GameObject player;
    public GameObject Player
    {
        get { return player; }
    }

    public static int attack;
    public static int weaponID;

    void Start()
    {
        // 槍の基本情報
        attack = 2;
        durable = 10;
        durable_max = 10;
        weaponID = 1;
    }

    public void DelWeaponDurable()
    {
        Debug.Log("durable=" + durable);
        if (durable <= 0) { return; }
        base.BreakWeaponCheck(1);
        Debug.Log("durable=" + durable);
        if (durable == 0)
        {
            GameObject player = this.transform.parent.parent.gameObject;
            player.GetComponent<PlayerCtrl>().WeaponChangeLeft();
            player.GetComponent<WeaponManager>().DeleteWeapon(weaponID - 1);
            durable = durable_max;
        }
    }

    void Update()
    {
        // スペースキーを押したら槍を投げる
        if (Input.GetKeyDown(KeyCode.Space))
        {

            var playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
            

            //剣に切り替え
            playerControl.ChangeWeapon(0);

            // 槍のストックを減らす
            playerControl.AddWeaponStock(-1, 1);

            //Instantiate(Sword, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            //Instantiate(Sword);

            
        }

        //// "Z"キーを押したら槍を消す
        //// 将来的にここは何かに当たったら消える処理にする
        //if (Input.GetKeyDown(KeyCode.Z)){
        //    Destroy(gameObject);
        //}

        if (this.transform.childCount != 0) { return; }

        GameObject ChildSpear = Instantiate(ShotSpear);
        ChildSpear.transform.parent = this.transform;
        ChildSpear.transform.localPosition = Vector3.zero;
        ChildSpear.GetComponent<Rigidbody>().isKinematic = true;
    }
}
