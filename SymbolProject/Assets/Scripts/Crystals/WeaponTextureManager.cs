using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponTextureManager : MonoBehaviour
{
    public Sprite spearSprite;
    public Sprite axSprite;
    public Sprite shieldSprite;
    public Sprite twinSwordSprite;
    public Sprite cymbalSprite;
    public Sprite hammerSprite;
    public Sprite meteoSprite;
    public Sprite questionSprite;
    public Sprite exclamation;

    private void Start()
    {
        gameObject.GetComponent<Image>().color = Color.clear;
    }

    private void Update()
    {
        WeaponChange();
    }

    public void WeaponChange()
    {
        if (GetComponent<WeaponInfo>().weaponList != WeaponInfo.WeaponList.sword)
        {
            gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        if (GetComponent<WeaponInfo>().weaponList == WeaponInfo.WeaponList.spear)
        {
            gameObject.GetComponent<Image>().sprite = spearSprite;
        }
        else if (GetComponent<WeaponInfo>().weaponList == WeaponInfo.WeaponList.ax)
        {
            gameObject.GetComponent<Image>().sprite = axSprite;
        }
        else if (GetComponent<WeaponInfo>().weaponList == WeaponInfo.WeaponList.shield)
        {
            gameObject.GetComponent<Image>().sprite = shieldSprite;
        }
        else if (GetComponent<WeaponInfo>().weaponList == WeaponInfo.WeaponList.twinSword)
        {
            gameObject.GetComponent<Image>().sprite = twinSwordSprite;
        }
        else if (GetComponent<WeaponInfo>().weaponList == WeaponInfo.WeaponList.cymbal)
        {
            gameObject.GetComponent<Image>().sprite = cymbalSprite;
        }
        else if (GetComponent<WeaponInfo>().weaponList == WeaponInfo.WeaponList.hammer)
        {
            gameObject.GetComponent<Image>().sprite = hammerSprite;
        }
        else if (GetComponent<WeaponInfo>().weaponList == WeaponInfo.WeaponList.meteo)
        {
            gameObject.GetComponent<Image>().sprite = meteoSprite;
        }
        else if (GetComponent<WeaponInfo>().weaponList == WeaponInfo.WeaponList.question)
        {
            gameObject.GetComponent<Image>().sprite = questionSprite;
        }
        else if (GetComponent<WeaponInfo>().weaponList == WeaponInfo.WeaponList.exclamation)
        {
            gameObject.GetComponent<Image>().sprite = exclamation;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.clear;
        }

    }
}
