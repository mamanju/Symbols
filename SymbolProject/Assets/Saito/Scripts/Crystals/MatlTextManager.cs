using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatlTextManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private int[] nowMatl = new int[4];

    private GameObject childText;

    void Start()
    {
        childText = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        nowMatl = player.GetComponent<MatlManager>().NowMatl;
        for (int i = 0; i < nowMatl.Length; i++)
        {
            if ( i == (int)this.GetComponent<MatlInfo>().matlList || this.GetComponent<MatlInfo>().matlList == MatlInfo.MatlList.empty)
            {
                childText.GetComponent<Text>().text = "×" + nowMatl[i].ToString();
            }
        }
    }
}
