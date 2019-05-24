using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //ジャンプ用変数
    private bool groundFlag;
    private bool downFlag;
    private float jumpForce = 10.0f;

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

    //playerのrigidbody
    private Rigidbody playerRb;

    //ポーズUI
    [SerializeField]
    private GameObject synthesisGUI;
    private GameObject matlBoxes;

    private bool attackFlag;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        matlBoxes = synthesisGUI.transform.GetChild(2).gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            synthesisGUI.SetActive(!synthesisGUI.activeSelf);
            if (synthesisGUI.activeSelf == true)
            {
                Time.timeScale = 0.0f;
                this.GetComponent<MatlManager>().HaveCrystal();
            }
            else
            {
                Time.timeScale = 1.0f;
                matlBoxes.GetComponent<MatlBox>().ReturnCrystals();
            }
        }
    }

    void FixedUpdate()
    {
        //現在のplayerの速度の取得
        playerVelocity = playerRb.velocity.magnitude;
        //addする値を0にリセット
        speedForce = Vector3.zero;

        //キー入力
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        //ピタッと止まる処理
        if (_horizontal + _vertical == 0)
        {
            if (stopFlag == true) { StopMove(); }
        }
        //スピード制御と、静止状態の維持
        if (playerVelocity >= speedMax || _horizontal + _vertical == 0)
        {
            speed = 0.0f;
        }
        else if (_horizontal != 0 || _vertical != 0)
        {
            speed = 50.0f;
            stopFlag = true;
        }
        //ダッシュ
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedMax = 10.0f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedMax = 5.0f;
        }

        speedForce = new Vector3(speed * _horizontal, 0.0f, speed * _vertical);
        playerRb.AddForce(forceMgmt * (speedForce - playerRb.velocity));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpRay();
            if (groundFlag == true)
            {
                groundFlag = false;
                downFlag = true;

                DoJump();
            }
        }

        //落下を自然にする処理
        if (downFlag == true)
        {
            playerRb.AddForce(0.0f, -10.0f, 0.0f);
        }

        //攻撃
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (attackFlag == true)
            {

            }
        }
    }

    public void StopMove()
    {
        stopFlag = false;
        playerRb.velocity = Vector3.zero;
    }
    
    public void JumpRay()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        float distance = 0.5f;

        Debug.DrawRay(ray.origin, ray.direction, Color.red, 1.0f);

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.tag == "Ground")
            {
                groundFlag = true;
                downFlag = false;
            }
        }
    }

    public void DoJump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

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
