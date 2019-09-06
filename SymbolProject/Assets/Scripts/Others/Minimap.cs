using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{

    public Transform player;
    [Range(0, 800)]
    public int h;
    [Range(0, 800)]
    public int w;

    void Start()
    {
        // Switch to 640 x 480 full-screen at 60 hz
        

    }

    private void LateUpdate()
    {
        //Screen.SetResolution(h, w, true, 60);
        Vector3 newposition = player.position;
        newposition.y = transform.position.y;
        transform.position = newposition;

        //transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
