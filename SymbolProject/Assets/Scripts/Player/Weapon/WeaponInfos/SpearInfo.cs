using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearInfo : WeaponController
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

        player = transform.parent.GetComponent<GetPlayer>().Player;

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

    public void InstantiateSpear()
    {
        if (this.transform.childCount != 0) { return; }
        GameObject ChildSpear = Instantiate(ShotSpear);
        ChildSpear.transform.parent = this.transform;
        ChildSpear.transform.localPosition = Vector3.zero;
        ChildSpear.transform.localEulerAngles = new Vector3(90, 0, 0);
        ChildSpear.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("槍");
        if (other.transform.tag == "Enemy")
        {
            player.GetComponent<PlayerCtrl>().Attack();
        }
    }
}
