using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFlashManager : MonoBehaviour
{
    private Image image;
    private Text text;

    private float alpha;
    private float flashSpeed = 1.5f;
    private float stopLimit = 0.3f;
    private float stopTime;
    private bool flashFlag;
    private bool isFlash;


    // Start is called before the first frame update
    void Start()
    {
        image = transform.GetChild(0).gameObject.GetComponent<Image>();
        text = transform.GetChild(1).gameObject.GetComponent<Text>();
        stopTime = stopLimit;
    }

    // Update is called once per frame
    void Update()
    {
        //1になった時少しの間表示を続ける
        if(isFlash)
        {
            stopTime -= Time.deltaTime;
        }

        if (stopTime <= 0)
        {
            isFlash = false;
            flashFlag = true;
            stopTime = stopLimit;
        }

        //0だったら加算、1だったら減算
        if(alpha <= 0)
        {
            flashFlag = false;
        }
        if(alpha >= 1 && !flashFlag)
        {
            isFlash = true;
        }
        
        //加算減算 flashSpeedで速度調整
        if (flashFlag && !isFlash)
        {
            alpha -= Time.deltaTime * flashSpeed;
        }
        else
        {
            alpha += Time.deltaTime * flashSpeed;
        }

        //alpha値を代入
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }
}
