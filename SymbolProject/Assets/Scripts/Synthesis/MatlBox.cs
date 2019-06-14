using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatlBox : MonoBehaviour
{
    private GameObject[] itemBox = new GameObject[4];
    private int itemBoxCount;
    private int selectNum;
    private int mobeCtrl = 4;

    [SerializeField]
    private GameObject player;

    private bool moveRightFlag;
    public bool MoveRightFlag
    {
        get { return moveRightFlag; }
        set { moveRightFlag = value; }
    }

    private bool moveLeftFlag;
    public bool MoveLeftFlag
    {
        get { return moveLeftFlag; }
        set { moveLeftFlag = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        itemBoxCount = this.transform.childCount;
        for (int i = 0; i < itemBoxCount; i++)
        {
            itemBox[i] = transform.GetChild(i).gameObject;
        }
        itemBox[0].GetComponent<Button>().Select();
        selectNum = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveRightFlag == true && selectNum < mobeCtrl)
        {
            if (selectNum != mobeCtrl - 1)
            {
                selectNum++;
            }
            else
            {
                selectNum = 3;
            }
        }
        if (moveLeftFlag == true && selectNum >= 0)
        {
            if (selectNum != 0)
            {
                selectNum--;
            }
            else
            {
                selectNum = 0;
            }
        }
        itemBox[selectNum].GetComponent<Button>().Select();
        
        moveRightFlag = false;
        moveLeftFlag = false;
    }
}
