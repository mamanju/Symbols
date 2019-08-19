using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl instance;

    //ジャンプ用変数
    private bool groundFlag;
    public bool GroundFlag
    {
        get { return groundFlag; }
        set { groundFlag = value; }
    }
    private bool downFlag;
    [SerializeField]
    private float jumpForce;
    private float downSpeed;

    private float nowPlayerY;

    //キー入力（WASD）
    private float _horizontal;
    private float _vertical;

    //移動用変数
    private float speed;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    private float playerVelocity;

    //横移動用変数
    private float angle;
    private Vector3 moveForceH;
    private Vector3 speedForce;

    private float lastSelect;

    private Vector3 horizontalForce;
    private Vector3 verticalForce;

    //playerのrigidbody
    private Rigidbody playerRb;

    //ポーズUI
    [SerializeField]
    private GameObject synthesisGUI;
    private GameObject matlBoxes;
    private GameObject matlButton;
    private GameObject synthesisBoxes;
    private GameObject synthesisCrystal;

    //カメラの向きを取得
    private Vector3 cameraForward;

    //武器の切り替えの処理
    private WeaponInfo nowWeapon;
    [SerializeField]
    private WeaponManager weaponManager;
    [SerializeField]
    private PlayerWeaponManager playerWeaponManager;
    private int weaponNumber = 0;
    public int GetWeaponNumber
    {
        get { return weaponNumber; }
    }

    private int weaponLength;
    [SerializeField]
    private Image nowWeapon_S;
    
    [SerializeField]
    private PlayerStatus playerStatus;
    
    //ノックバック
    private KnockBack knockBack;

    //特殊攻撃、槍とCymbals
    [SerializeField]
    private GameObject spear;
    [SerializeField]
    private GameObject cymbals;
    private WeaponAtaccks weaponAtaccks;

    //ノックバック
    //無敵時間
    private bool knockbackFlag = false;
    public bool KnockBackFlag
    {
        get { return knockbackFlag; }
        set { knockbackFlag = value; }
    }

    // 移動アニメーション
    private Animator playerAnime;
    private string key_Jump = "Jump";
    private string key_Speed = "Speed";

    // 攻撃アニメーション
    private string key_Weapon = "Weapons";
    private string key_Attack = "Attack";
    private string key_AnimeBack = "AnimeBack";
    private bool attackAnime_Flag = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        synthesisGUI.SetActive(false);
        playerRb = GetComponent<Rigidbody>();
        matlButton = synthesisGUI.transform.GetChild(1).gameObject;
        matlBoxes = synthesisGUI.transform.GetChild(2).gameObject;
        synthesisBoxes = synthesisGUI.transform.GetChild(3).gameObject;
        synthesisCrystal = synthesisGUI.transform.GetChild(4).gameObject;

        nowWeapon = nowWeapon_S.gameObject.GetComponent<WeaponInfo>();
        nowWeapon.weaponList = WeaponInfo.WeaponList.sword;
        weaponLength = weaponManager.NowWeapon.Length;

        knockBack = GetComponent<KnockBack>();

        playerAnime = GetComponent<Animator>();
        playerAnime.SetBool(key_Jump, false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetButtonDown("Triangle"))
        {
            synthesisGUI.SetActive(!synthesisGUI.activeSelf);
            if (synthesisGUI.activeSelf == true)
            {
                Time.timeScale = 0.0f;
                this.GetComponent<MatlManager>().HaveCrystal();
                this.GetComponent<WeaponManager>().HaveWeapon();
            }
            else
            {
                Time.timeScale = 1.0f;
            }
        }

        if (synthesisGUI.activeSelf == true)
        {
            SynthesisCtrl synthesisCtrl = synthesisBoxes.GetComponent<SynthesisCtrl>();
            if (Input.GetAxisRaw("CrossKey_H") > 0 && lastSelect == 0 && synthesisCtrl.EndFlag == false
                || Input.GetKeyDown(KeyCode.RightArrow))
            {
                matlButton.GetComponent<MatlBox>().MoveRightFlag = true;
            }
            if (Input.GetAxisRaw("CrossKey_H") < 0 && lastSelect == 0 && synthesisCtrl.EndFlag == false
                || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                matlButton.GetComponent<MatlBox>().MoveLeftFlag = true;
            }
            if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Square"))
            {
                synthesisCtrl.EndFlag = true;
            }
            //リセット
            if (Input.GetButtonDown("Cross") || Input.GetKeyDown(KeyCode.I))
            {
                synthesisCtrl.ResetCrystal();
            }
        }

        lastSelect = Input.GetAxisRaw("CrossKey_H");

        if (Time.timeScale == 0) { return; }
        
        //武器切り替え
        if (Input.GetButtonDown("L1") || Input.GetKeyDown(KeyCode.L))
        {
            WeaponChangeRight();
        }
        if (Input.GetButtonDown("R1") || Input.GetKeyDown(KeyCode.K))
        {
            WeaponChangeLeft();
        }

        //槍を投げる（通常攻撃じゃない)
        if (Input.GetButtonDown("R2") || Input.GetKeyDown(KeyCode.O))
        {
            weaponAtaccks = spear.transform.GetChild(0).GetComponent<WeaponAtaccks>();
            weaponAtaccks.AbnormalAttaks(weaponNumber);
        }

        //攻撃
        if (Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("Circle"))
        {
            playerAnime.SetTrigger(key_Attack);
            attackAnime_Flag = true;

            GetComponent<weapon_collider>().OnCollider(weaponNumber);
            
        }
    }

    void FixedUpdate()
    {
        if (knockBack.KnockbackFlag == true) { return; }

        cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        //現在のplayerの速度の取得
        playerVelocity = playerRb.velocity.magnitude;
        //addする値を0にリセット
        speedForce = Vector3.zero;
        moveForceH = Vector3.zero;
        angle = 0;
        
        horizontalForce = new Vector3(transform.right.x, 0.0f, transform.right.z);
        verticalForce = new Vector3(transform.forward.x, 0.0f, transform.forward.z);
        
        //キー入力
        _horizontal = Input.GetAxis("Horizontal_L");
        _vertical = Input.GetAxis("Vertical_L");
        
        if (_horizontal != 0 || _vertical != 0)
        {
            //左スティック押し込みに変更
            if (Input.GetButton("StickPush_L"))
            {
                speed = runSpeed;
            }
            else
            {
                speed = walkSpeed;
            }
            speedForce += cameraForward.normalized * _vertical 
                + Camera.main.transform.right.normalized * _horizontal;
            speedForce = speedForce * speed * Time.deltaTime;

            if (knockbackFlag != true)
            {
                playerRb.velocity = new Vector3 (speedForce.x, playerRb.velocity.y, speedForce.z);
            }
        }
        
        if (speedForce != Vector3.zero && _horizontal + _vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(speedForce);
        }

        // アニメーション
        if (4 <= playerVelocity)
        {
            // 走るアニメーション
            playerAnime.SetFloat(key_Speed, 1.1f);
        }
        else if (0 < playerVelocity && playerVelocity < 4)
        {
            // 歩くアニメーション
            playerAnime.SetFloat(key_Speed, 0.6f);
        }
        else
        {
            // 静止アニメーション
            playerAnime.SetFloat(key_Speed, 0.0f);
        }


        //ジャンプ
        if (groundFlag == true)
        {
            //playerAnime.SetBool(key_Jump, false);
            if (Input.GetButtonDown("Cross") || Input.GetKeyDown(KeyCode.Space))
            {
                playerAnime.SetTrigger(key_Jump);
                playerRb.velocity = new Vector3 (playerRb.velocity.x,
                    transform.up.y * jumpForce * Time.deltaTime, playerRb.velocity.z);
                Debug.Log(new Vector3(playerRb.velocity.x,
                    transform.up.y * jumpForce * Time.deltaTime, playerRb.velocity.z));
                downFlag = true;
            }
        }
    }

    /// <summary>
    /// 武器切り替え処理
    /// </summary>
    /// <param name="_num">切り替える武器が何番目か</param>
    public void ChangeWeapon(int _num)
    {
        weaponNumber = _num;
        nowWeapon.weaponList = (WeaponInfo.WeaponList)(_num);
        playerWeaponManager.WeaponObjChange(_num);
        playerStatus.WeaponAttack(_num);

        playerAnime.SetInteger(key_Weapon, _num);

        //if (_num == 6)
        //{
        //    searchingBehavior.M_searchAngle = 360;
        //    searchingBehavior.ApplySearchAngle();
        //}
        //else
        //{
        //    searchingBehavior.M_searchAngle = 90;
        //    searchingBehavior.ApplySearchAngle();
        //}
    }

    //武器切り替え右
    public void WeaponChangeRight()
    {
        weaponNumber = (weaponNumber + 1) % (weaponLength + 1);
        while (weaponNumber != 0 && weaponManager.NowWeapon[weaponNumber - 1] == 0)
        {
            weaponNumber++;
            if (weaponNumber >= weaponLength)
            {
                weaponNumber = 0;
            }
        }
        ChangeWeapon(weaponNumber);
    }

    //武器切り替え左
    public void WeaponChangeLeft()
    {
        weaponNumber -= 1;
        if (weaponNumber < 0)
        {
            weaponNumber = weaponLength;
        }
        while (weaponNumber != 0 && weaponManager.NowWeapon[weaponNumber - 1] == 0)
        {
            weaponNumber--;
            if (weaponNumber <= 0)
            {
                weaponNumber = 0;
            }
        }
        ChangeWeapon(weaponNumber);
    }

    //範囲内に敵がいたら攻撃
    //武器の耐久値の減少
    public void Attack(GameObject other)
    {
        DownDurable();
        if (playerStatus.NowWeaponID == 5)
        {
            weaponAtaccks = cymbals.GetComponent<WeaponAtaccks>();
            weaponAtaccks.AbnormalAttaks(weaponNumber);
        }
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().Damage(playerStatus.PlayerAttack());
        }
        
        if (playerStatus.NowWeaponID == 2)
        {
             //切って橋にする木のタグ
             if(other.tag == "Tree")
             {
                 other.GetComponent<CutTreeController>().SetFallFlag = true;
             }
        }
        else if (playerStatus.NowWeaponID == 5)
        {
             //成長するギミックの木のタグ
             if(other.tag == "Tree")
             {
                 //中身よろしくお願いします！！！！
                 //タグの変更もお願いしますm(__)m
             }
          
        }
    }

    //武器の耐久値減少
    public void DownDurable()
    {
        playerWeaponManager.WeaponDel(playerStatus.NowWeaponID);
    }
}
