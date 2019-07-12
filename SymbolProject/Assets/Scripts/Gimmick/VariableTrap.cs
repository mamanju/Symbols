using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//時間で変わる炎のギミックのスクリプト（時間でColliderとEffectはActiveとDesactiveに変わるので、Desactiveの時プレイヤーはダメージを受けないで、進める）
public class VariableTrap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FireOff()
    {
        gameObject.SetActive(false);
    }
}
