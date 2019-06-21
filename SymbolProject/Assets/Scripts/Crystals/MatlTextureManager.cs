using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatlTextureManager : MonoBehaviour
{
    [SerializeField]
    private Sprite stickSprite;
    [SerializeField]
    private Sprite triangleSprite;
    [SerializeField]
    private Sprite lessThanSprite;
    [SerializeField]
    private Sprite circleSprite;

    private void Start()
    {
        this.GetComponent<Image>().color = Color.clear;
    }

    private void Update()
    {
        MatlChange();
    }

    public void MatlChange()
    {
        MatlInfo matlInfo = this.GetComponent<MatlInfo>();
        Image image = this.GetComponent<Image>();
        if (matlInfo.matlList != MatlInfo.MatlList.empty)
        {
            image.color = new Color(255, 255, 255, 255);
        }
        if (matlInfo.matlList == MatlInfo.MatlList.stick)
        {
            image.sprite = stickSprite;
        }
        else if (matlInfo.matlList == MatlInfo.MatlList.triangle)
        {
            image.sprite = triangleSprite;
        }
        else if (matlInfo.matlList == MatlInfo.MatlList.lessThan)
        {
            image.sprite = lessThanSprite;
        }
        else if (matlInfo.matlList == MatlInfo.MatlList.circle)
        {
            image.sprite = circleSprite;
        }
        else
        {
            image.color = Color.clear;
        }

    }

   
}
