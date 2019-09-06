using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponText : MonoBehaviour
{
    private Text weaponText;

    [SerializeField]
    private WeaponInfo nowWeapon;

    private string[] sentence = new string[10];
    private string nowSentence;

    [SerializeField]
    private float speed = 0.05f;
    private float text_speed;
    private int sentenceNum;

    private int lastWeapon;

    private bool start_count;
    private bool end_display;

    // Start is called before the first frame update
    void Start()
    {
        text_speed = speed;
        WeaponTextSet();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastWeapon != (int)nowWeapon.weaponList)
        {
            sentenceNum = 0;
            end_display = false;
        }

        lastWeapon = (int)nowWeapon.weaponList;

        if (sentence[(int)nowWeapon.weaponList].Length != sentenceNum - 1 && end_display == false)
        {
            start_count = true;
        }
        else
        {
            start_count = false;
            end_display = true;
        }

        if (end_display == true) { return; }
        if (start_count == false) { return; }

        text_speed -= Time.deltaTime;

        if (text_speed <= 0)
        {
            DisplayText();
            text_speed = speed;
            sentenceNum++;
        }
    }

    public void WeaponTextSet()
    {
        sentence[(int)WeaponInfo.WeaponList.sword] = "【ソード】\nただの剣。\nなぜか壊れない。";
        sentence[(int)WeaponInfo.WeaponList.spear] = "【スピア】\nR2で投げられそうだ。";
        sentence[(int)WeaponInfo.WeaponList.ax] = "【アックス】\n木が切れそうだ。";
        sentence[(int)WeaponInfo.WeaponList.shield] = "【シールド】\nすべての攻撃が防げる。";
        //WeaponInfo.WeaponList.twinSword
        sentence[(int)WeaponInfo.WeaponList.cymbal] = "【シンバル】\n大きな音で敵がびっくりする。\n木が好みそうな音色だ。";
        //WeaponInfo.WeaponList.hammer
        //WeaponInfo.WeaponList.meteor
        //WeaponInfo.WeaponList.question
        //WeaponInfo.WeaponList.exclamation
    }

    public void DisplayText()
    {
            GetComponent<Text>().text = sentence[(int)nowWeapon.weaponList].Substring(0, sentenceNum);
    }
}
