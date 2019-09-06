using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCrystal : MonoBehaviour
{
    private float time_crystal = 0;
    [SerializeField]
    private float timeMax_crystal = 0.5f;
    private bool flag_crystal;

    // Start is called before the first frame update
    void Start()
    {
        time_crystal = timeMax_crystal;
        flag_crystal = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 3, 0);

        if (flag_crystal == false) { return; }

        if (flag_crystal == true)
        {
            time_crystal -= Time.deltaTime;
        }
        if (time_crystal <= 0)
        {
            flag_crystal = false;
            time_crystal = timeMax_crystal;
            GetComponent<CapsuleCollider>().enabled = true;
        }
    }
}
