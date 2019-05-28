using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatlInfo : MonoBehaviour
{
    [SerializeField]
    public enum MatlList
    {
        empty,
        stick,
        triangle,
        lessThan,
        circle,
        plus,
        noCrystal,
    };

    public MatlList matlList;
   
}
