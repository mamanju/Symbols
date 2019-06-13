using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//時間で変わる炎のギミックのスクリプト（時間でColliderとEffectはActiveとDesactiveに変わるので、Desactiveの時プレイヤーはダメージを受けないで、進める）
public class VariableTrap : MonoBehaviour
{
    private VariableTrap vTrap;
    [SerializeField]
    private Collider firecol;

    private bool activeFlag = false;

    public bool ActiveFlag {
        get;set;
    }

    // Start is called before the first frame update
    void Start()
    {
        firecol = this.GetComponent<Collider>();
    }
    public void FireOn()
    {

        firecol.enabled = true;
    }

    public void FireOff()
    {
        firecol.gameObject.SetActive(false);
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
