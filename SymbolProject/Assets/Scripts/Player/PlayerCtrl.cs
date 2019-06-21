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
    private float rotateSpeed = 0.628319f;

    Vector3 horizontalForce;
    Vector3 verticalForce;

    //playerのrigidbody
    private Rigidbody playerRb;

    //ポーズUI
    [SerializeField]
    private GameObject synthesisGUI;
    private GameObject matlBoxes;

    private bool attackFlag;

    void Start()
    {
        synthesisGUI.SetActive(false);
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

        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Rotate(new Vector3(transform.rotation.x, - rotateSpeed, transform.rotation.z));
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Rotate(new Vector3(transform.rotation.x, rotateSpeed, transform.rotation.z));
        }
    }

    void FixedUpdate()
    {
        //現在のplayerの速度の取得
        playerVelocity = playerRb.velocity.magnitude;
        //addする値を0にリセット
        speedForce = Vector3.zero;

        //ダッシュ
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedMax = 10.0f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedMax = 5.0f;
        }

        horizontalForce = new Vector3(transform.right.x, 0.0f, transform.right.z);
        verticalForce = new Vector3(transform.forward.x, 0.0f, transform.forward.z);

        //キー入力
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        Debug.Log(_horizontal);
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
        }
        else
        {
            stopFlag = true;
        }

        speedForce = horizontalForce * speed * _horizontal + verticalForce * speed * _vertical;
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
        float distance = 1.5f;

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
