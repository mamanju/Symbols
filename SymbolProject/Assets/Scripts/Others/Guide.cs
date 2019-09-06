using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Guide : MonoBehaviour
{
    private Text guideText;

    private string[] guide = new string[3];

    void Start()
    {
        guideText = GetComponent<Text>();
    }

    void Update()
    {
        
    }

    private void GuideSentence()
    {
        guide[0] = "この木は斧を使えば切れそうだ";
        guide[1] = "この木は□ボタンで登れそうだ";
        guide[2] = "あのクリスタルは槍を使えば破壊出来そうだ";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "")
        {
            // この木は斧を使えば切れそうだ
        }

        if (other.tag == "ClimbTree")
        {
            // この木は□ボタンで登れそうだ
        }

        if (other.tag == "FireSwitch")
        {
            // あのクリスタルは槍を使えば破壊出来そうだ
        }
    }
}
