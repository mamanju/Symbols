using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//スイッチで変わる炎のギミック（スイッチを押したら、炎のギミックはDesactiveになって,プレイヤーは進むことができる)
public class StaticTrap : MonoBehaviour
{
    public static StaticTrap instance;
    public Collider firecol;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        firecol = this.GetComponent<Collider>();
    }
    public void FireOn()
    {

        firecol.enabled = true;
    }

    public void FireOff()
    {
        firecol.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FireOn();
            PlayerController.instance.playerhp--;
        }
    }
}
