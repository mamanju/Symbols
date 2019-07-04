using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAtaccks : MonoBehaviour
{
    private GameObject player;

    void Update()
    {
        if (spearTimeFlag == true)
        {
            spearTime -= Time.unscaledDeltaTime;
        }
        if (spearTime <= 0)
        {
            spearTimeFlag = false;
            spearTime = 10;
            SpearEnd();
        }
    }

    public void AbnormalAttaks(int _num)
    {
        if(_num == 1)
        {
            SpearShot();
        }
        if (_num == 5)
        {
            CymbalsFalter();
        }
    }

    [SerializeField]
    private float _startSpeed = 5.0f;
    private bool spearFlag = false;
    private bool spearTimeFlag = false;
    private float spearTime = 10;
    
    public void SpearShot()
    {
        spearTimeFlag = true;
        spearFlag = true;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        var direction = transform.forward;
        direction.y += 1f;
        rigidbody.AddForce(transform.up * _startSpeed, ForceMode.Impulse);
    }


    //槍

    //槍が消えて個数が減ってどうのこうの
    private void SpearEnd()
    {
        spearFlag = false;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        if (rigidbody.isKinematic == true)
        {
            player = this.transform.parent.GetComponent<SpearInfo>().Player;
            player.GetComponent<WeaponManager>().NowWeapon[0]--;
            player.GetComponent<PlayerCtrl>().WeaponChangeLeft();
        }

        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (spearFlag == true)
        {
            //敵
            if (collision.transform.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyController>().Damage(1);

            }
            //Switch
            else if (collision.transform.tag == "TrapSwitch")
            {
                collision.gameObject.GetComponent<VariableTrapSwitch>().StopFire();
            }
            
            SpearEnd();
        }
    }


    //シンバル

    private bool stunFlag;
    public bool StunFlag
    {
        get { return stunFlag; }
        set { stunFlag = value; }
    }

    public void CymbalsFalter()
    {
        player = this.transform.parent.parent.gameObject;
        Finder finder = player.GetComponent<Finder>();

        if (finder.M_enemy.Count == 0) { return; }
        for (int i = 0; i < finder.M_enemy.Count; i++)
        {
            finder.M_enemy[i].GetComponent<Rigidbody>().isKinematic = true;
            stunFlag = true;
        }
    }


    //盾

    public bool Defense()
    {
        player = this.transform.parent.parent.gameObject;
        Finder finder = player.GetComponent<Finder>();

        if (finder.M_enemy.Count == 0) { return false; }
        return true;
    }

}
