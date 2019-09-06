using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPTextureManager : MonoBehaviour
{
    [SerializeField]
    private Sprite fullHpSprites;
    [SerializeField]
    private Sprite halfHpSprites;
    [SerializeField]
    private Sprite notHpSprites;

    [SerializeField]
    private GameObject player;

    // hpをPlayerStatusから持ってくる
    private int HP;

    private GameObject[] HPBox = new GameObject[10];
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            HPBox[i] = this.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HP = PlayerStatus.PlayerHp();
        if(HP == 0)
        {
            for(int i = 0; i < 10; i++)
            {
                HPBox[i].GetComponent<Image>().sprite = notHpSprites;
            }
            return;
        }
        if (HP % 2 == 1)
        {
            HP -= 1;
            HPBox[HP / 2].GetComponent<Image>().sprite = halfHpSprites;            
        }
        else if (HP % 2 == 0 && HP < 20)
        {
            HPBox[HP / 2].GetComponent<Image>().sprite = notHpSprites;
        }
        for (int i = 0; i < HP / 2 - 1; i++)
        {
            HPBox[i].GetComponent<Image>().sprite = fullHpSprites;
        }

        for (int i = HP / 2 + 1; i < 10; i++)
        {
            HPBox[i].GetComponent<Image>().sprite = notHpSprites;
        }


    }
}
