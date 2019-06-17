using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatlTextureManager : MonoBehaviour
{
    public Sprite stickSprite;
    public Sprite triangleSprite;
    public Sprite lessThanSprite;
    public Sprite circleSprite;

    private void Start()
    {
        gameObject.GetComponent<Image>().color = Color.clear;
    }

    private void Update()
    {
        MatlChange();
    }

    public void MatlChange()
    {
        if (GetComponent<MatlInfo>().matlList != MatlInfo.MatlList.empty)
        {
            gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        if (GetComponent<MatlInfo>().matlList == MatlInfo.MatlList.stick)
        {
            gameObject.GetComponent<Image>().sprite = stickSprite;
        }
        else if (GetComponent<MatlInfo>().matlList == MatlInfo.MatlList.triangle)
        {
            gameObject.GetComponent<Image>().sprite = triangleSprite;
        }
        else if (GetComponent<MatlInfo>().matlList == MatlInfo.MatlList.lessThan)
        {
            gameObject.GetComponent<Image>().sprite = lessThanSprite;
        }
        else if (GetComponent<MatlInfo>().matlList == MatlInfo.MatlList.circle)
        {
            gameObject.GetComponent<Image>().sprite = circleSprite;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.clear;
        }

    }

   
}
