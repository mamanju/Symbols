using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//時間で変わる炎のギミックのスクリプト（時間でColliderとEffectはActiveとDesactiveに変わるので、Desactiveの時プレイヤーはダメージを受けないで、進める）
public class VariableTrap : MonoBehaviour
{
    public static VariableTrap instance;
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
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            FireOff();
        }
    }
}
