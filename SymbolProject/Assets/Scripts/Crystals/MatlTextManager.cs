using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatlTextManager : MonoBehaviour
{
    private GameObject player;
    private int[] nowMatl = new int[4];

    private GameObject childText;

    void Start()
    {
        childText = this.transform.GetChild(0).gameObject;
        player = transform.parent.parent.GetComponent<GetPlayer>().Player;
    }

    // Update is called once per frame
    void Update()
    {
        nowMatl = MatlManager.NowMatl;
        for (int i = 0; i < nowMatl.Length; i++)
        {
            if ( i == (int)this.GetComponent<MatlInfo>().matlList)
            {
                childText.GetComponent<Text>().text = "×" + nowMatl[i].ToString();
            }
            else if (this.GetComponent<MatlInfo>().matlList == MatlInfo.MatlList.empty)
            {
                childText.GetComponent<Text>().text = "×0";
            }
        }
    }
}
