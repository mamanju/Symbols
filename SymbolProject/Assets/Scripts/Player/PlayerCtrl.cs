using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    //ジャンプ用変数
    private bool groundFlag;
    public bool GroundFlag
    {
        get { return groundFlag; }
        set { groundFlag = value; }
    }
    private bool downFlag;
    private float jumpForce = 4.0f;
    private float downSpeed;

    private float nowPlayerY;

    //キー入力（WASD）
    private float _horizontal;
    private float _vertical;

    //移動用変数
    private bool stopFlag;
    private float speed;
    private float speedMax = 2.0f;
    private float forceMgmt = 2.0f;
    private float playerVelocity;

    //横移動用変数
    private float angle;
    private Vector3 moveForceH;
    private Vector3 speedForce;
    private float rotateSpeed = 0.628319f;

    private float lastHorizontal;
    private float lastVertical;

    private float lastSelect;

    Vector3 horizontalForce;
    Vector3 verticalForce;

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
    private int weaponLength;
    [SerializeField]
    private Image nowWeapon_S;
    
    [SerializeField]
    private PlayerStatus playerStatus;
    
    //武器切り替えによるコライダーの範囲の変更
    [SerializeField]
    private SearchingBehavior searchingBehavior;
    //敵のオブジェクト
    [SerializeField]
    private Finder finder;

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
    //HPが減る処理

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
            if (Input.GetAxisRaw("R2") == 1 && lastSelect == 0 && synthesisCtrl.EndFlag == false
                || Input.GetKeyDown(KeyCode.RightArrow))
            {
                matlButton.GetComponent<MatlBox>().MoveRightFlag = true;
            }
            if (Input.GetAxisRaw("R2") == -1 && lastSelect == 0 && synthesisCtrl.EndFlag == false
                || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                matlButton.GetComponent<MatlBox>().MoveLeftFlag = true;
            }
            if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Jump"))
            {
                synthesisCtrl.StartSynthesis = true;
                synthesisCtrl.EndFlag = true;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                synthesisCtrl.ResetFlag = true;
            }
        }

        lastSelect = Input.GetAxisRaw("Dpad_H");

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
        //キーの変更お願いします！！！！
        if (Input.GetKeyDown(KeyCode.O))
        {
            weaponAtaccks = spear.transform.GetChild(0).GetComponent<WeaponAtaccks>();
            weaponAtaccks.AbnormalAttaks(weaponNumber);
        }

        //攻撃
        if (Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("Circle"))
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        if (knockBack.KnockbackFlag == true) { return; }

        if ( lastHorizontal + lastVertical == 0 && stopFlag == true && groundFlag == true)
        {
            StopMove();
        }
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

        //ピタッと止まる処理
        if (_horizontal + _vertical == 0)
        {
            if (stopFlag == true) { StopMove(); }
        }
        //スピード制御と、静止状態の維持
        if (playerVelocity >= speedMax)
        {
            speed = 0.0f;
        }
        else if (_horizontal != 0 || _vertical != 0)
        {
            speed = 10.0f;
            stopFlag = true;

            if (Mathf.Abs(_horizontal ) + Mathf.Abs(_vertical) >= 1.0f)
            {
                speedMax = 5.0f;
            }
            else
            {
                speedMax = 2.0f;
            }
        }
        
        lastHorizontal = Mathf.Abs(_horizontal);
        lastVertical = Mathf.Abs(_vertical);
        speedForce += cameraForward * _vertical * speed + Camera.main.transform.right * speed * _horizontal;

        if (knockbackFlag != true)
        {
            playerRb.AddForce(forceMgmt * speedForce);
        }
        
        if (speedForce != Vector3.zero && _horizontal + _vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(speedForce);
        }

        //ジャンプ
        if (groundFlag == true)
        {
            downFlag = false;
            downSpeed = -1.3f;
            if (Input.GetButtonDown("Cross"))
            {
                nowPlayerY = transform.position.y + 3.0f;
                playerRb.velocity = new Vector3(playerRb.velocity.x, jumpForce, playerRb.velocity.z);
                downFlag = true;
            }
        }

        //落下を自然にする処理
        if (downFlag == true)
        {
            downSpeed *= 1.03f;
            playerRb.AddForce(0.0f, downSpeed, 0.0f);
        }
    }

    //移動ストップ
    public void StopMove()
    {
        stopFlag = false;
        playerRb.velocity = Vector3.zero;
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
        
        if (_num == 6)
        {
            searchingBehavior.M_searchAngle = 360;
            searchingBehavior.ApplySearchAngle();
        }
        else
        {
            searchingBehavior.M_searchAngle = 90;
            searchingBehavior.ApplySearchAngle();
        }
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
    public void Attack()
    {
        Debug.Log(playerStatus.PlayerAttack());
        Debug.Log(playerStatus.NowWeaponID);
        if (finder.M_enemy.Count + finder.M_tellain.Count == 0) { return; }
        DownDurable();
        if (playerStatus.NowWeaponID == 5)
        {
            weaponAtaccks = cymbals.GetComponent<WeaponAtaccks>();
            weaponAtaccks.AbnormalAttaks(weaponNumber);
        }
        for (int i = 0; i < finder.M_enemy.Count; i++)
        {
            finder.M_enemy[i].GetComponent<EnemyController>().Damage(playerStatus.PlayerAttack());
        }

        if (finder.M_enemy.Count != 0) { return; }

        if (playerStatus.NowWeaponID == 2)
        {
            for (int i = 0; i < finder.M_tellain.Count; i++)
            {
                //切って橋にする木のタグ
                if(finder.M_tellain[i].tag == "Tree")
                {
                    //中身よろしくお願いします！！！！
                    //タグの変更もお願いしますm(__)m
                }
            }
        }
        else if (playerStatus.NowWeaponID == 5)
        {
            for (int i = 0; i < finder.M_tellain.Count; i++)
            {
                //成長するギミックの木のタグ
                if(finder.M_tellain[i].tag == "Tree")
                {
                    //中身よろしくお願いします！！！！
                    //タグの変更もお願いしますm(__)m
                }
            }
        }
    }

    //武器の耐久値減少
    public void DownDurable()
    {
        playerWeaponManager.WeaponDel(playerStatus.NowWeaponID);
    }
}
