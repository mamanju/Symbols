using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponTextureManager : MonoBehaviour
{
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
    [SerializeField]
    private SynthesisController synthesisCtrl;

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
        bool isEmpty = thisWeaponInfo.weaponList != WeaponInfo.WeaponList.empty;
        bool isName = this.gameObject.name == "SynthesisCrystal";
        if (isEmpty && isName)
        {
            if (synthesisCtrl.EndFlag == false)
            {
                thisImage.color = new Color(1, 1, 1, 0.5f);
            }
            else
            {
                thisImage.color = Color.white;
            }
        }
        else if (isEmpty && isName == false)
        {
            thisImage.color = Color.white;
        }
        if (thisWeaponInfo.weaponList == WeaponInfo.WeaponList.spear)
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
