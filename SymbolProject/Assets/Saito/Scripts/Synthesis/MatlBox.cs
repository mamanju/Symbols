using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatlBox : MonoBehaviour
{
    private List<GameObject> itemBoxList = new List<GameObject>();
    private int itemBoxCount;
    private int selectNum;
    private int mobeCtrl = 15;

    [SerializeField]
    private GameObject playerItem;

    // Start is called before the first frame update
    void Start()
    {
        itemBoxCount = this.transform.childCount;
        for (int i = 0; i < itemBoxCount; i++)
        {
            itemBoxList.Add(transform.GetChild(i).gameObject);
        }

        itemBoxList[0].GetComponent<Button>().Select();
        selectNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (selectNum != mobeCtrl - 1 && selectNum != itemBoxCount - 1)
            {
                selectNum++;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (selectNum != 0 && selectNum != mobeCtrl)
            {
                selectNum--;
            }
     
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectNum - mobeCtrl >= 0)
            {
                selectNum -= mobeCtrl;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectNum + mobeCtrl < itemBoxCount)
            {
                selectNum += mobeCtrl;
            }
        }
        itemBoxList[selectNum].GetComponent<Button>().Select();
    }
    
    public void ReturnCrystals()
    {
        for (int i = 0; i < itemBoxCount; i++)
        {
            playerItem.GetComponent<MatlManager>().AddCrystal(
                (int)itemBoxList[i].GetComponent<MatlInfo>().matlList);
            itemBoxList[i].GetComponent<MatlInfo>().matlList = MatlInfo.MatlList.empty;
        }
    }
}
