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
        MatlInfo thisMatlInfo = this.GetComponent<MatlInfo>();
        MatlManager plyerMatlManager = player.GetComponent<MatlManager>();
        if (thisMatlInfo.matlList != MatlInfo.MatlList.empty)
        {
            for (int i = 0; i < 5; i++)
            {
                if (setMatl[i].GetComponent<MatlInfo>().matlList == MatlInfo.MatlList.empty)
                {
                    setMatl[i].GetComponent<MatlInfo>().matlList = thisMatlInfo.matlList;
                    if (thisMatlInfo.matlList == MatlInfo.MatlList.stick)
                    {
                        plyerMatlManager.NowMatl[0] -= 1;
                        if (plyerMatlManager.NowMatl[0] == 0)
                        {
                            thisMatlInfo.matlList = MatlInfo.MatlList.empty;
                        }
                    }
                    if (thisMatlInfo.matlList == MatlInfo.MatlList.triangle)
                    {
                        plyerMatlManager.NowMatl[1] -= 1;
                        if (plyerMatlManager.NowMatl[1] == 0)
                        {
                            thisMatlInfo.matlList = MatlInfo.MatlList.empty;
                        }
                    }
                    if (thisMatlInfo.matlList == MatlInfo.MatlList.lessThan)
                    {
                        plyerMatlManager.NowMatl[2] -= 1;
                        if (plyerMatlManager.NowMatl[2] == 0)
                        {
                            thisMatlInfo.matlList = MatlInfo.MatlList.empty;
                        }
                    }
                    if (thisMatlInfo.matlList == MatlInfo.MatlList.circle)
                    {
                        plyerMatlManager.NowMatl[3] -= 1;
                        if (plyerMatlManager.NowMatl[3] == 0)
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
