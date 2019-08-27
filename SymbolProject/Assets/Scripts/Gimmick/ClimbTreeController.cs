using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbTreeController : MonoBehaviour
{
    private bool climbFlag = false;

    public bool ClimbFlag {
        get { return climbFlag; }
        set { climbFlag = value; }
    }

    [SerializeField]
    private GameObject climbPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Climb(GameObject player) {
        climbFlag = true;
        player.transform.position = climbPos.transform.position;
    }
}
