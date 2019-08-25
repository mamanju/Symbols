using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAtaccks : MonoBehaviour
{
    private PlayerCtrl pCon;

    private GameObject player;

    void Update()
    {
        if (spearTimeFlag == false) { return; }

        if (spearTimeFlag == true)
        {
            spearTime -= Time.unscaledDeltaTime;
        }
        if (spearTime <= 0)
        {
            spearTimeFlag = false;
            spearTime = 10;
            //Destroy(gameObject);
            SpearEnd();
        }
    }

    public void AbnormalAttaks(int _num , GameObject _enemy)
    {
        if(_num == 1)
        {
            SpearShot();
        }
        if (_num == 5)
        {
            CymbalsFalter(_enemy);
        }
    }

    [SerializeField]
    private float _startSpeed = 5.0f;
    private bool spearFlag = false;
    private bool spearTimeFlag = false;
    private float spearTime = 5;

    private GameObject SpearBox;
    
    public void SpearShot()
    {
        GetComponent<BoxCollider>().enabled = true;
        SpearBox = this.transform.parent.gameObject;
        Debug.Log(SpearBox.name);
        this.transform.parent = null;
        spearTimeFlag = true;
        spearFlag = true;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        player = PlayerCtrl.instance.gameObject;
        transform.rotation = player.transform.rotation;
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        var direction = transform.forward;
        direction.y += 1f;
        rigidbody.AddForce(transform.up * _startSpeed, ForceMode.Impulse);
    }

    //槍が消えて個数が減ってどうのこうの
    private void SpearEnd()
    {
        Debug.Log("End" + SpearBox.name);
        spearFlag = false;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        if (rigidbody.isKinematic == true)
        {
            player = SpearBox.GetComponent<SpearInfo>().Player;
            WeaponManager.NowWeapon[0]--;
            player.GetComponent<PlayerCtrl>().WeaponChangeLeft();
        }

        pCon = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        // 槍の投げる動作をできるようにする
        pCon.SecondSpearPreventFlag = true;
        Destroy(gameObject);
        SpearBox.GetComponent<SpearInfo>().InstantiateSpear();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (spearFlag == true)
        {
            //敵
            if (collision.transform.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyController>().Damage(1);
                SpearEnd();
            }
            //Switch
            else if (collision.transform.tag == "FireSwitch")
            {
                collision.gameObject.GetComponent<VariableTrapSwitch>().StopFire();
                SpearEnd();
            }
            
        }
    }

    private bool stunFlag;
    public bool StunFlag
    {
        get { return stunFlag; }
        set { stunFlag = value; }
    }

    public void CymbalsFalter(GameObject _enemy)
    {
        //player = this.transform.parent.parent.gameObject;
        //Finder finder = player.GetComponent<Finder>();

        //if (finder.M_enemy.Count == 0) { return; }
        //for (int i = 0; i < finder.M_enemy.Count; i++)
        
        _enemy.GetComponent<Rigidbody>().isKinematic = true;
        stunFlag = true;
        
    }
}
