using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour
{
    private int weaponNumber;
    private int weaponLength;
    private PlayerStatus pStatus;
    private float invincibleTime = 1.0f;

    private bool invincibleFlag = false;

    SpriteRenderer MainSpriteRenderer;

    [SerializeField]
    private Image nowWeapon_S;


    [SerializeField]
    Sprite[] WeaponSprites;


    

    void Start()
    {
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        pStatus = GetComponent<PlayerStatus>();
        weaponLength = PlayerStatus.Weapon.GetValues(typeof(PlayerStatus.Weapon)).Length;
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

        Debug.Log(invincibleTime);
        if(invincibleFlag == true)
        { 
            invincibleTime -= Time.deltaTime;
            if (invincibleTime <= 0)
            {
                invincibleTime = 1;
                invincibleFlag = false;
            }
        }
    }

    /// <summary>
    /// 武器切り替え処理
    /// </summary>
    /// <param name="num">切り替える武器が何番目か</param>
    public void ChangeWeapon(int num)
    {
        weaponNumber = num;
        pStatus.nowWeapon = (PlayerStatus.Weapon)(num);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if(pStatus.PlayerHp == 0 || invincibleFlag == true) { return; }
            invincibleFlag = true;
            pStatus.PlayerHp -= 1;
            Debug.Log("ダメージを受けたよ！");
            Debug.Log(pStatus.PlayerHp);
        }
    }

}
