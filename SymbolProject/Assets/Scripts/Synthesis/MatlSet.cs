using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatlSet : MonoBehaviour
{
    [SerializeField]
    private GameObject setMatls;
    private GameObject[] setMatl = new GameObject[5];

    private GameObject player;
    private int[] nowMatl = new int[4];

    private int stickCount;
    private int triangleCount;
    private int lessThanCount;
    private int circleCount;

    void Start()
    {
        player = transform.parent.parent.GetComponent<GetPlayer>().Player;
        for(int i = 0; i < 5; i++)
        {
            setMatl[i] = setMatls.transform.GetChild(i).gameObject;
        }
    }

    public void OnSelect()
    {
        if (setMatls.GetComponent<SynthesisController>().EndFlag == true) { return; }
        MatlInfo thisMatlInfo = this.GetComponent<MatlInfo>();
        if (thisMatlInfo.matlList != MatlInfo.MatlList.empty)
        {
            for (int i = 0; i < 5; i++)
            {
                if (setMatl[i].GetComponent<MatlInfo>().matlList == MatlInfo.MatlList.empty)
                {
                    setMatl[i].GetComponent<MatlInfo>().matlList = thisMatlInfo.matlList;
                    if (thisMatlInfo.matlList == MatlInfo.MatlList.stick)
                    {
                        MatlManager.NowMatl[0] -= 1;
                        if (MatlManager.NowMatl[0] == 0)
                        {
                            thisMatlInfo.matlList = MatlInfo.MatlList.empty;
                        }
                    }
                    if (thisMatlInfo.matlList == MatlInfo.MatlList.triangle)
                    {
                        MatlManager.NowMatl[1] -= 1;
                        if (MatlManager.NowMatl[1] == 0)
                        {
                            thisMatlInfo.matlList = MatlInfo.MatlList.empty;
                        }
                    }
                    if (thisMatlInfo.matlList == MatlInfo.MatlList.lessThan)
                    {
                        MatlManager.NowMatl[2] -= 1;
                        if (MatlManager.NowMatl[2] == 0)
                        {
                            thisMatlInfo.matlList = MatlInfo.MatlList.empty;
                        }
                    }
                    if (thisMatlInfo.matlList == MatlInfo.MatlList.circle)
                    {
                        MatlManager.NowMatl[3] -= 1;
                        if (MatlManager.NowMatl[3] == 0)
                        {
                            thisMatlInfo.matlList = MatlInfo.MatlList.empty;
                        }
                    }
                    break;
                }
            }
        }
    }
}
