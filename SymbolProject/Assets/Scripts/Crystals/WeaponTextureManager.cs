using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponTextureManager : MonoBehaviour
{
    public Sprite emptySprite;
    public Sprite spearSprite;
    public Sprite axSprite;
    public Sprite shieldSprite;

    private void Start()
    {
        gameObject.GetComponent<Image>().sprite = emptySprite;
    }

    private void Update()
    {
        WeaponChange();
    }

    public void WeaponChange()
    {
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
        else
        {
            gameObject.GetComponent<Image>().sprite = emptySprite;
        }

    }
}
