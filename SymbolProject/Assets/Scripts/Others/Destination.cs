using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{

    [SerializeField]
    private Transform cursor;
    [SerializeField]
    private Transform growTree;
    [SerializeField]
    private Transform climbTree;
    [SerializeField]
    private Transform firstBridge;
    [SerializeField]
    private Transform secondBridge;
    [SerializeField]
    private Transform crystal;
    [SerializeField]
    private Transform bossGate;

    void Start()
    {
        
    }

    void Update()
    {
        cursor.LookAt(firstBridge);
    }
}
