using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool attackFlag;
    public bool AttackFlag
    {
        get { return attackFlag; }
        set { attackFlag = value; }
    }

    //武器の切り替えの処理
    //ノックバック
    //無敵時間
    //HPが減る処理

    void Start()
    {
        synthesisGUI.SetActive(false);
        playerRb = GetComponent<Rigidbody>();
        matlButton = synthesisGUI.transform.GetChild(1).gameObject;
        matlBoxes = synthesisGUI.transform.GetChild(2).gameObject;
        synthesisBoxes = synthesisGUI.transform.GetChild(3).gameObject;
        synthesisCrystal = synthesisGUI.transform.GetChild(4).gameObject;
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
            if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Cross"))
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
    }

    void FixedUpdate()
    {
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

        Debug.Log(_horizontal);
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
        playerRb.AddForce(forceMgmt * speedForce);
        
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

        //武器切り替え
        if (Input.GetButtonDown("L1"))
        {

        }
        if (Input.GetButtonDown("R1"))
        {

        }

        //攻撃
        if (Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("Circle"))
        {
            if (attackFlag == true)
            {

            }
        }
    }

    //移動ストップ
    public void StopMove()
    {
        stopFlag = false;
        playerRb.velocity = Vector3.zero;
    }
}
