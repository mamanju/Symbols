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
    private GameObject spear_p;

    SpriteRenderer MainSpriteRenderer;

    [SerializeField]
    private Image nowWeapon_S;


    [SerializeField]
    Sprite[] WeaponSprites;

    [SerializeField]
    private Transform spearPos;

    private Vector3 playerPosition;

    

    void Start()
    {
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        pStatus = GetComponent<PlayerStatus>();
        weaponLength = PlayerStatus.Weapon.GetValues(typeof(PlayerStatus.Weapon)).Length;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            weaponNumber = (weaponNumber + 1) % weaponLength;
            ChangeWeapon(weaponNumber);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            weaponNumber -= 1;
            if (weaponNumber < 0)
            {
                weaponNumber = weaponLength - 1;
            }
            ChangeWeapon(weaponNumber);
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            if(pStatus.nowWeapon == PlayerStatus.Weapon.Spear) {
                playerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                var spear = Instantiate(spear_p);
                
                spear.transform.position = spearPos.transform.position;
                Debug.Log(transform.rotation);
                spear.transform.rotation = transform.rotation;
                spear.transform.GetChild(0).GetComponent<ArrowAction>().Shot(transform.forward);
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
            Debug.Log("ダメージを受けた！");
            Debug.Log(pStatus.PlayerHp -= 1);        }
    }

}
