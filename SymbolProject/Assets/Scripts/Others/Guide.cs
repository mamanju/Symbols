using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Guide : MonoBehaviour
{
    private TextMesh guideText;

    private string[] guide = new string[4];

    void Start()
    {
        GuideSentence();
        guideText = GetComponent<TextMesh>();
        transform.localScale = new Vector3(-1, 1, 1);
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    private void GuideSentence()
    {
        guide[0] = "この木は斧を使えば切れそうだ";
        guide[1] = "この木は□ボタンで登れそうだ";
        guide[2] = "あのクリスタルは槍を使えば破壊出来そうだ";
        guide[3] = "";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "FallTree")
        {
            guideText.text = guide[0];
        }

        if (other.tag == "ClimbTree")
        {
            guideText.text = guide[1];
        }

        if (other.tag == "FireSwitch")
        {
            guideText.text = guide[2];
        }
    }

    private void OnTriggerExit(Collider other)
    {

        guideText.text = guide[3];
    }
}
