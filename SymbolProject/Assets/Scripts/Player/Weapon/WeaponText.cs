using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponText : MonoBehaviour
{
    private Text weaponText;
    private string[] sentence;

    [SerializeField]
    private WeaponInfo nowWeapon;

    [SerializeField]
    private float displayTime = 0.05f;

    private int currentSentenceNum = 0;
    private string currentSentence = string.Empty;
    private float timeUntilDisplay = 0;
    private float timeBeganDisplay = 1;
    private int lastUpdateCharCount = -1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        WeaponTextChange();
        //GetComponent<Text>().text = weaponText.text;
        
    }

    public void SetNextSentencee()
    {
        currentSentence = sentence[currentSentenceNum];
        timeUntilDisplay = currentSentence.Length * displayTime;
        timeBeganDisplay = Time.time;
        currentSentenceNum++;
        lastUpdateCharCount = 0;
    }

    public bool IsDisplayComplete()
    {
        return Time.time > timeBeganDisplay + timeUntilDisplay;
    }

    public void WeaponTextChange()
    {
        if (nowWeapon.weaponList == WeaponInfo.WeaponList.sword)
        {
            weaponText.text = "[ソード]\nただの剣。\nなぜか壊れない。";
        }
        else if (nowWeapon.weaponList == WeaponInfo.WeaponList.spear)
        {
            weaponText.text = "[スピア]\nR2で投げられそうだ。";
        }
        else if (nowWeapon.weaponList == WeaponInfo.WeaponList.ax)
        {
            weaponText.text = "[アックス]\n木が切れそうだ。";
        }
        else if (nowWeapon.weaponList == WeaponInfo.WeaponList.shield)
        {
            weaponText.text = "[シールド]\nすべての攻撃が防げる。";
        }
        //else if (nowWeapon.weaponList == WeaponInfo.WeaponList.twinSword)
        //{
        //  weaponText.GetComponent<Text>().text = "";
        //}
        else if (nowWeapon.weaponList == WeaponInfo.WeaponList.cymbal)
        {
            weaponText.text = "[シンバル]\n大きな音で敵がびっくりする。\n木が好みそうな音色だ。";
        }
        //else if (nowWeapon.weaponList == WeaponInfo.WeaponList.hammer)
        //{
        //  weaponText.GetComponent<Text>().text = "";
        //}
        //else if (nowWeapon.weaponList == WeaponInfo.WeaponList.meteor)
        //{
        //  weaponText.GetComponent<Text>().text = "";
        //}
        //else if (nowWeapon.weaponList == WeaponInfo.WeaponList.question)
        //{
        //  weaponText.GetComponent<Text>().text = "";
        //}
        //else if (nowWeapon.weaponList == WeaponInfo.WeaponList.exclamation)
        //{
        //  weaponText.GetComponent<Text>().text = "";
        //}
    }
}
