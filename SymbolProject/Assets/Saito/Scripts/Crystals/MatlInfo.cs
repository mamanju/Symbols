using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatlInfo : MonoBehaviour
{
    [SerializeField]
    public enum MatlList
    {
        empty = -1,
        stick,
        triangle,
        lessThan,
        circle,
    };

    public MatlList matlList;  
}
