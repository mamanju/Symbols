using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private int weaponNumber;
    private int weaponLength;
    private PlayerStatus pStatus;
    [SerializeField]
    private float invincibleTime = 0.5f;
    [SerializeField]
    private float knockbackTime = 0.05f;
    [SerializeField]
    private float hitJump = 0.0f;
    private bool invincibleFlag = false;
    private bool ctrlFlag = false;
    private bool knockbackFlag = false;

    private float invincibleTimeReset;
    private float knockbackTimeReset;
    


    SpriteRenderer MainSpriteRenderer;

    [SerializeField]
    private Image nowWeapon_S;
    

    [SerializeField]
    Sprite[] WeaponSprites;

    Rigidbody playerRb;

    //キー入力（WASD）
    private float _horizontal;
    private float _vertical;

    //移動用変数
    private bool stopFlag;
    private float speed;
    private float speedMax = 5.0f;
    private float forceMgmt = 2.0f;
    private float playerVelocity;
    private Vector3 speedForce;
    private float rotateSpeed = 0.628319f;

    Vector3 horizontalForce;
    Vector3 verticalForce;



    void Start()
    {
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        pStatus = GetComponent<PlayerStatus>();
        weaponLength = PlayerStatus.Weapon.GetValues(typeof(PlayerStatus.Weapon)).Length;
        playerRb = GetComponent<Rigidbody>();
        invincibleTimeReset = invincibleTime;
        knockbackTimeReset = knockbackTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            weaponNumber = (weaponNumber + 1) % weaponLength;
            ChangeWeapon(weaponNumber);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            weaponNumber -= 1;
            if (weaponNumber < 0)
            {
                weaponNumber = weaponLength - 1;
            }
            ChangeWeapon(weaponNumber);
        }

        if(invincibleFlag == true)
        { 
            invincibleTime -= Time.deltaTime;
            if (invincibleTime <= 0)
            {
                invincibleTime = invincibleTimeReset;
                invincibleFlag = false;
            }
            if (knockbackFlag == true)
            {
                knockbackTime -= Time.deltaTime;
                if (knockbackTime <= 0)
                {
                    knockbackTime = knockbackTimeReset;
                    knockbackFlag = false;
                }
            }
        }

        speedForce = Vector3.zero;

        horizontalForce = new Vector3(transform.right.x, 0.0f, transform.right.z);
        verticalForce = new Vector3(transform.forward.x, 0.0f, transform.forward.z);

        //キー入力
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        //スピード制御と、静止状態の維持
        if (playerVelocity >= speedMax || _horizontal + _vertical == 0)
        {
            speed = 0.0f;
        }
        else if (_horizontal != 0 || _vertical != 0)
        {
            speed = 50.0f;
        }
        else
        {
            stopFlag = true;
        }

        speedForce = horizontalForce * speed * _horizontal + verticalForce * speed * _vertical;
        if (ctrlFlag != true && knockbackFlag != true)
        {
            playerRb.AddForce(forceMgmt * (speedForce - playerRb.velocity));
        }
    }

    /// <summary>
    /// 武器切り替え処理
    /// </summary>
    /// <param name="num">切り替える武器が何番目か</param>
    public void ChangeWeapon(int num)
    {
        weaponNumber = num;
        pStatus.nowWeapon = (PlayerStatus.Weapon)(num) - 1;
        Debug.Log(pStatus.nowWeapon);
        nowWeapon_S.sprite = WeaponSprites[num];
    }

    /// <summary>
    /// 武器増減処理
    /// </summary>
    /// <param name="addNum">増減する値</param>
    /// <param name="wNum">何の武器が増減するか</param>
    public void AddWeaponStock(int addNum, int wNum)
    {
        pStatus.WeaponStock[wNum] += addNum;
    }



    // コライダーじゃなくて攻撃で減るようにしてね！
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if(pStatus.PlayerHp == 0 || invincibleFlag == true) { return; }
            invincibleFlag = true;
            ctrlFlag = true;
            knockbackFlag = true;

            pStatus.PlayerHp -= 1;

            Vector3 knockback = new Vector3(-transform.forward.x, hitJump, -transform.forward.z);
            
            playerRb.AddForce(knockback * 10,ForceMode.Impulse);

            ctrlFlag = false;
            playerRb.velocity = Vector3.zero;

        }
    }
}
