using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //ジャンプ用変数
    private bool groundFlag;
    private bool downFlag;
    private float jumpForce = 5.0f;
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
    private Vector3 speedForce;

    private float lastHorizontal;
    private float lastVertical;

    private float lastSelect;
    private bool lastJump;

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
        playerRb = GetComponent<Rigidbody>();
        JumpRay();
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
            }
            if (Input.GetButtonDown("Attack") && lastJump == false)
            {
                synthesisCrystal.GetComponent<SetSynthesisCrystal>().WeaponMove = true;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                synthesisBoxes.GetComponent<SynthesisCtrl>().ResetFlag = true;
            }
        }

        lastSelect = Input.GetAxisRaw("Dpad_H");
        lastJump = Input.GetButtonDown("Jump");
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(_horizontal) + Mathf.Abs(_vertical) < 1
            && lastHorizontal + lastVertical > Mathf.Abs(_horizontal) + Mathf.Abs(_vertical)
            && groundFlag != false)
        {
            StopMove();
        }

        cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        //現在のplayerの速度の取得
        playerVelocity = playerRb.velocity.magnitude;
        //addする値を0にリセット
        speedForce = Vector3.zero;
        
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
        speedForce += cameraForward * _vertical * speed + Camera.main.transform.right * _horizontal * speed;
        playerRb.AddForce(forceMgmt * speedForce);

        if (speedForce != Vector3.zero && _horizontal + _vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(speedForce);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (groundFlag == true)
            {
                nowPlayerY = transform.position.y + 3.0f;
                groundFlag = false;
                playerRb.velocity = new Vector3(playerRb.velocity.x, jumpForce, playerRb.velocity.z);
                downFlag = true;
            }
        }

        if (groundFlag == false) { JumpRay(); }

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
   
    public void StopMove()
    {
        stopFlag = false;
        playerRb.velocity = Vector3.zero;
    }

    public void JumpRay()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        float distance = 1.2f;
    
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 1.0f);
    
        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.tag == "Ground")
            {
                groundFlag = true;
                downFlag = false;
                downSpeed = -1.3f;
            }
        }
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
