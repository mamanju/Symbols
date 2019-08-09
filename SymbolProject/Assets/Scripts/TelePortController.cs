using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelePortController : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift)) {
            Player.transform.position = transform.position;
        }
    }
}
