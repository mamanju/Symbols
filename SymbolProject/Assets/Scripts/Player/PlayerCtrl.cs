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
    private GameObject synthesisBoxes;
    private GameObject synthesisCrystal;

    //カメラの向きを取得
    private Vector3 cameraForward;

    private bool attackFlag;

    void Start()
    {
        synthesisGUI.SetActive(false);
        playerRb = GetComponent<Rigidbody>();
        matlBoxes = synthesisGUI.transform.GetChild(1).gameObject;
        synthesisBoxes = synthesisGUI.transform.GetChild(2).gameObject;
        synthesisCrystal = synthesisGUI.transform.GetChild(3).gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetButtonDown("Synthesis"))
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
            if (Input.GetAxisRaw("Dpad_H") == 1 && lastSelect == 0)
            {
                matlBoxes.GetComponent<MatlBox>().MoveRightFlag = true;
            }
            if (Input.GetAxisRaw("Dpad_H") == -1 && lastSelect == 0)
            {
                matlBoxes.GetComponent<MatlBox>().MoveLeftFlag = true;
            }
            if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Jump"))
            {
                synthesisBoxes.GetComponent<SynthesisCtrl>().StartSynthesis = true;
                synthesisBoxes.GetComponent<SynthesisCtrl>().EndFlag = true;
            }
            if (Input.GetButtonDown("Attack") && synthesisBoxes.GetComponent<SynthesisCtrl>().EndFlag == true)
            {
                synthesisCrystal.GetComponent<SetSynthesisCrystal>().WeaponMove = true;
                synthesisBoxes.GetComponent<SynthesisCtrl>().EndFlag = false;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                synthesisBoxes.GetComponent<SynthesisCtrl>().ResetFlag = true;
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
        _vertical = Input.GetAxis("Vertical");
        
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
            if (Input.GetButtonDown("Jump"))
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


        //攻撃
        if (Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("Attack"))
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

    //攻撃用
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            attackFlag = true;
        }
        else
        {
            attackFlag = false;
        }
    }
}
