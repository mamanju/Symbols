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
        guide[0] = "斧を使えば切れそうだ";
        guide[1] = "□ボタンで登れそうだ";
        guide[2] = "槍を使えば破壊出来そうだ";
    }
}
