using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//時間で変わる炎のギミックのスクリプト（時間でColliderとEffectはActiveとDesactiveに変わるので、Desactiveの時プレイヤーはダメージを受けないで、進める）
public class VariableTrap : MonoBehaviour
{

    private AudioSource audio;

    void Start()
    {
        audio = transform.parent.GetComponent<AudioSource>();
    }

    public void FireOff()
    {
        gameObject.SetActive(false);
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            audio.enabled = true;
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            audio.enabled = false;
        }
    }

}
