using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

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
    private bool resetFlag;

    //カメラの向きを取得
    private Vector3 cameraForward;

    //武器の切り替えの処理
    private WeaponInfo nowWeapon;
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
    
    // ノックバック
    private KnockBack knockBack;

    // 攻撃連打防止用
    private bool attackFlag = true;
    public bool AttackFlag
    {
        set { attackFlag = value; }
    }

    // 木登り用
    private BoxCollider boxCollider;
    private bool climbFlag = false;
    private float climbClliderTime;
    private float max_climbClliderTime = 0.1f;
    private bool climbClliderTimeFlag = false;

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
    private string key_Climb = "Climb";

    // 攻撃アニメーション
    private string key_Weapon = "Weapons";
    private string key_Attack = "Attack";
    private string key_ShildAttack = "ShildAttack";
    private string key_AnimeBack = "AnimeBack";
    private string key_SpearThrow = "SpearThrow";
    private string key_ShildLoop = "ShildLoop";

    [SerializeField]
    private float spearThrowTime = 2.0f;
    private float max_spearThrowTime;
    private bool spearThrowTimeFlag = false;
    // 槍を投げている時に槍を投げる動作をするとエラーが出るので防ぐ為のFlag
    private bool secondSpearPreventFlag = true;
    public bool SecondSpearPreventFlag
    {
        get { return secondSpearPreventFlag; }
        set { secondSpearPreventFlag = value; }
    }


    // trueの時はダメージを受けない
    private bool shildFlag = false;
    public bool ShildFlag
    {
        get { return shildFlag; }
    }

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
        weaponLength = WeaponManager.NowWeapon.Length;

        knockBack = GetComponent<KnockBack>();

        playerAnime = GetComponent<Animator>();
        playerAnime.SetBool(key_Jump, false);

        max_spearThrowTime = spearThrowTime;

        boxCollider = GetComponent<BoxCollider>();
        climbClliderTime = max_climbClliderTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetButtonDown("Triangle") && synthesisGUI.activeSelf == false)
        {
            synthesisGUI.SetActive(!synthesisGUI.activeSelf);
            if (synthesisGUI.activeSelf == true)
            {
                Time.timeScale = 0.0f;
                this.GetComponent<MatlManager>().HaveCrystal();
                this.GetComponent<WeaponManager>().HaveWeapon();
            }
        }

        if (Input.GetButtonDown("Cross") && synthesisGUI.activeSelf == true)
        {
            synthesisGUI.SetActive(!synthesisGUI.activeSelf);
            Time.timeScale = 1.0f;
            resetFlag = false;
        }


        //合成の操作
        if (synthesisGUI.activeSelf == true)
        {
            SynthesisController synthesisCtrl = synthesisBoxes.GetComponent<SynthesisController>();
            //素材クリスタルを右に移動
            if (Input.GetAxisRaw("CrossKey_H") > 0 && lastSelect == 0 && synthesisCtrl.EndFlag == false
                || Input.GetKeyDown(KeyCode.RightArrow))
            {
                matlButton.GetComponent<MatlSelect>().MoveRightFlag = true;
            }
            //素材クリスタルを左に移動
            if (Input.GetAxisRaw("CrossKey_H") < 0 && lastSelect == 0 && synthesisCtrl.EndFlag == false
                || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                matlButton.GetComponent<MatlSelect>().MoveLeftFlag = true;
            }
            //合成ボタン
            if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Square"))
            {
                synthesisCtrl.EndFlag = true;
            }
            //リセット
            if (Input.GetButtonDown("Triangle") || Input.GetKeyDown(KeyCode.I))
            {
                if (resetFlag)
                {
                    synthesisCtrl.ResetCrystal();
                }
            }
            lastSelect = Input.GetAxisRaw("CrossKey_H");
            if (resetFlag == false) { resetFlag = true; }
        }
        
        if (Time.timeScale == 0) { return; }
        
        //武器切り替え
        if (Input.GetButtonDown("R1") || Input.GetKeyDown(KeyCode.L))
        {
            WeaponChangeRight();
        }
        if (Input.GetButtonDown("L1") || Input.GetKeyDown(KeyCode.K))
        {
            WeaponChangeLeft();
        }

        //槍を投げる（通常攻撃じゃない)
        if (secondSpearPreventFlag && weaponNumber == 1 && Input.GetButtonDown("R2") || Input.GetKeyDown(KeyCode.O))
        {
            playerAnime.SetTrigger(key_SpearThrow);
            spearThrowTimeFlag = true;
            secondSpearPreventFlag = false;
        }
        // 槍を投げる
        if (spearThrowTimeFlag)
        {
            spearThrowTime -= Time.deltaTime;
            if (spearThrowTime <= 0)
            {
                weaponAtaccks = spear.transform.GetChild(0).GetComponent<WeaponAtaccks>();
                weaponAtaccks.AbnormalAttaks(weaponNumber, null);
                spearThrowTimeFlag = false;
                spearThrowTime = max_spearThrowTime;
            }
        }


        if (weaponNumber == 3)
        {
            if (Input.GetButton("Circle"))
            {
                // shildFlagがtrueの時はダメージを受けない
                Debug.Log("押した");
                shildFlag = true;
                playerAnime.SetBool(key_ShildAttack, true);
                playerAnime.SetBool(key_ShildLoop, true);
            }

            if (Input.GetButtonUp("Circle"))
            {
                Debug.Log("離した");
                shildFlag = false;
                playerAnime.SetBool(key_ShildAttack, false);
                playerAnime.SetBool(key_ShildLoop, false);
            }
        }

        //攻撃
        if ((Input.GetButtonDown("Circle")) && weaponNumber != 3 && attackFlag)
        {
            attackFlag = false;
            playerAnime.SetTrigger(key_Attack);
            //GetComponent<weapon_collider>().OnCollider(weaponNumber);
            GetComponent<weapon_collider>().SetCollider_Flag = true;
            //DownDurable();
        }


        // 木登り
        if (Input.GetButtonDown("Square"))
        {
            boxCollider.enabled = true;
            climbFlag = true;
            climbClliderTimeFlag = true;
        }
        // 木登り用のコライダーを一瞬だけ出して消す
        if (climbClliderTimeFlag)
        {
            climbClliderTime -= Time.deltaTime;
            if (climbClliderTime <= 0)
            {
                boxCollider.enabled = false;
                climbFlag = false;
                climbClliderTime = max_climbClliderTime;
            }
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
            if (Input.GetButtonDown("Cross") || Input.GetKeyDown(KeyCode.Space))
            {
                playerAnime.SetTrigger(key_Jump);
                playerRb.velocity = new Vector3 (playerRb.velocity.x,
                    transform.up.y * jumpForce * Time.deltaTime, playerRb.velocity.z);
                Player_SoundManager.instance.PlaySE_player(4);
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
        while (weaponNumber != 0 && WeaponManager.NowWeapon[weaponNumber - 1] == 0)
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
        while (weaponNumber != 0 && WeaponManager.NowWeapon[weaponNumber - 1] == 0)
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
        //DownDurable();
        if(weaponNumber < 3 || weaponNumber == 5)
        {
            Player_SoundManager.instance.PlaySE_player(weaponNumber);
        }
        if (playerStatus.NowWeaponID == 5 && other.tag == "Enemy")
        {
            weaponAtaccks = cymbals.GetComponent<WeaponAtaccks>();
            weaponAtaccks.AbnormalAttaks(weaponNumber, other);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ClimbTree" && climbFlag)
        {
        Debug.Log("木");
            playerAnime.SetTrigger(key_Climb);
            other.GetComponent<ClimbTreeController>().Climb(gameObject);
            boxCollider.enabled = false;
            climbFlag = false;
        }
        else
        {
            boxCollider.enabled = false;
            climbFlag = false;
        }
    }
}
