using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatlSet : MonoBehaviour
{
    [SerializeField]
    private GameObject setMatls;
    private GameObject[] setMatl = new GameObject[5];

    [SerializeField]
    private GameObject player;
    private int[] nowMatl = new int[4];

    private int stickCount;
    private int triangleCount;
    private int lessThanCount;
    private int circleCount;

    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            setMatl[i] = setMatls.transform.GetChild(i).gameObject;
        }
    }

    public void OnSelect()
    {
        if (setMatls.GetComponent<SynthesisCtrl>().EndFlag == true) { return; }
        if (this.GetComponent<MatlInfo>().matlList != MatlInfo.MatlList.empty)
        {
            for (int i = 0; i < 5; i++)
            {
                if (setMatl[i].GetComponent<MatlInfo>().matlList == MatlInfo.MatlList.empty)
                {
                    setMatl[i].GetComponent<MatlInfo>().matlList = this.GetComponent<MatlInfo>().matlList;
                    if (this.GetComponent<MatlInfo>().matlList == MatlInfo.MatlList.stick)
                    {
                        player.GetComponent<MatlManager>().NowMatl[0] -= 1;
                        if (player.GetComponent<MatlManager>().NowMatl[0] == 0)
                        {
                            this.GetComponent<MatlInfo>().matlList = MatlInfo.MatlList.empty;
                        }
                    }
                    if (this.GetComponent<MatlInfo>().matlList == MatlInfo.MatlList.triangle)
                    {
                        player.GetComponent<MatlManager>().NowMatl[1] -= 1;
                        if (player.GetComponent<MatlManager>().NowMatl[1] == 0)
                        {
                            this.GetComponent<MatlInfo>().matlList = MatlInfo.MatlList.empty;
                        }
                    }
                    if (this.GetComponent<MatlInfo>().matlList == MatlInfo.MatlList.lessThan)
                    {
                        player.GetComponent<MatlManager>().NowMatl[2] -= 1;
                        if (player.GetComponent<MatlManager>().NowMatl[2] == 0)
                        {
                            this.GetComponent<MatlInfo>().matlList = MatlInfo.MatlList.empty;
                        }
                    }
                    if (this.GetComponent<MatlInfo>().matlList == MatlInfo.MatlList.circle)
                    {
                        player.GetComponent<MatlManager>().NowMatl[3] -= 1;
                        if (player.GetComponent<MatlManager>().NowMatl[3] == 0)
                        {
                            this.GetComponent<MatlInfo>().matlList = MatlInfo.MatlList.empty;
                        }
                    }
                    break;
                }
            }
        }
    }
}
