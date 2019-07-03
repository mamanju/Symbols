using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponTextureManager : MonoBehaviour
{
    [SerializeField]
    private Sprite swordSprite;
    [SerializeField]
    private Sprite spearSprite;
    [SerializeField]
    private Sprite axSprite;
    [SerializeField]
    private Sprite shieldSprite;
    [SerializeField]
    private Sprite twinSwordSprite;
    [SerializeField]
    private Sprite cymbalSprite;
    [SerializeField]
    private Sprite hammerSprite;
    [SerializeField]
    private Sprite meteoSprite;
    [SerializeField]
    private Sprite questionSprite;
    [SerializeField]
    private Sprite exclamation;

    private void Start()
    {
        this.GetComponent<Image>().color = Color.clear;
    }

    private void Update()
    {
        WeaponChange();
    }

    public void WeaponChange()
    {
        WeaponInfo thisWeaponInfo = this.GetComponent<WeaponInfo>();
        Image thisImage = this.GetComponent<Image>();
        if (thisWeaponInfo.weaponList != WeaponInfo.WeaponList.empty)
        {
            thisImage.color = new Color(255, 255, 255, 255);
        }
        if (thisWeaponInfo.weaponList == WeaponInfo.WeaponList.sword)
        {
            thisImage.sprite = swordSprite;
        }
        else if (thisWeaponInfo.weaponList == WeaponInfo.WeaponList.spear)
        {
            thisImage.sprite = spearSprite;
        }
        else if (thisWeaponInfo.weaponList == WeaponInfo.WeaponList.ax)
        {
            thisImage.sprite = axSprite;
        }
        else if (thisWeaponInfo.weaponList == WeaponInfo.WeaponList.shield)
        {
            thisImage.sprite = shieldSprite;
        }
        else if (thisWeaponInfo.weaponList == WeaponInfo.WeaponList.twinSword)
        {
            thisImage.sprite = twinSwordSprite;
        }
        else if (thisWeaponInfo.weaponList == WeaponInfo.WeaponList.cymbal)
        {
            thisImage.sprite = cymbalSprite;
        }
        else if (thisWeaponInfo.weaponList == WeaponInfo.WeaponList.hammer)
        {
            thisImage.sprite = hammerSprite;
        }
        else if (thisWeaponInfo.weaponList == WeaponInfo.WeaponList.meteor)
        {
            thisImage.sprite = meteoSprite;
        }
        else if (thisWeaponInfo.weaponList == WeaponInfo.WeaponList.question)
        {
            thisImage.sprite = questionSprite;
        }
        else if (thisWeaponInfo.weaponList == WeaponInfo.WeaponList.exclamation)
        {
            thisImage.sprite = exclamation;
        }
        else
        {
            thisImage.color = Color.clear;
        }

    }
}
