using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatlSet : MonoBehaviour
{
    [SerializeField]
    private GameObject[] setMatl = new GameObject[5];

    public void OnSelect()
    {
        if (this.GetComponent<MatlInfo>().matlList != MatlInfo.MatlList.empty)
        {
            for (int i = 0; i < 5; i++)
            {
                if (setMatl[i].GetComponent<MatlInfo>().matlList == MatlInfo.MatlList.empty)
                {
                    setMatl[i].GetComponent<MatlInfo>().matlList = this.GetComponent<MatlInfo>().matlList;
                    this.GetComponent<MatlInfo>().matlList = MatlInfo.MatlList.empty;
                    break;
                }
            }
        }
    }
}
