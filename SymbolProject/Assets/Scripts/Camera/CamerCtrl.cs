using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Vector3 offset;

    private float distance = 7.0f;
    private float angle1;
    private float angle2;

    private float distanceT;

    private bool rayJudg;


    [SerializeField]
    private float speed = 1.0f;
    private float radianP;

    private bool cameraUp;
    private bool cameraDown;

    private Vector3 lookPos;

    // Usve this for initialization
    void Start()
    {
        lookPos = player.transform.position + new Vector3(0, 1, 0);

        offset = transform.position - lookPos;
        
        angle1 = Mathf.Acos(Vector3.Dot(player.transform.forward, offset.normalized));
        radianP = player.transform.eulerAngles.y * (Mathf.PI / 180.0f) + 3.14174f;
        angle2 = radianP;

        rayJudg = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Time.timeScale == 0) { return; }

        lookPos = player.transform.position + new Vector3(0, 1, 0);
        
        radianP = player.transform.eulerAngles.y * (Mathf.PI / 180.0f) + 3.14174f;
        
        Ray ray = new Ray(lookPos, offset);
        RaycastHit hit;
        int distanceR = 7;
        
        //Rayの可視化
        Debug.DrawLine(ray.origin, ray.direction, Color.red);
       
        if (Physics.Raycast(ray, out hit, distanceR))
        {
            if (hit.transform.tag == "Terrain")
            {
                distance = Vector3.Distance(hit.point, lookPos);
            }
        }
        else
        {
            distance = 7.0f;
        }

        float _horizontalR = Input.GetAxisRaw("Horizontal_R");
        float _verticalR = Input.GetAxisRaw("Vertical_R");
        //左右移動
        if (_horizontalR != 0)
        {
            angle2 -= Time.deltaTime * speed * _horizontalR;
            if (angle2 > 6.28319) { angle2 -= 6.28319f; };
            if (angle2 <= 0) { angle2 += 6.28319f; };
        }
        //上に移動
        if (_verticalR > 0 || Input.GetKey(KeyCode.UpArrow))
        {
            if (angle1 > 1.3f && cameraUp == true) { angle1 = 1.3f; cameraDown = false; }
            else if (angle1 < 1.3f) { angle1 += Time.deltaTime * speed; cameraUp = true; }
        }
        //下に移動
        if (_verticalR < 0 || Input.GetKey(KeyCode.DownArrow))
        {
           if (angle1 < 0.1f && cameraDown == true) { angle1 = 0.1f; cameraDown = false; }
            else if (angle1 >= 0.1f) { angle1 -= Time.deltaTime * speed; cameraUp = true; }
        }
        //Reset
        if(Input.GetKeyDown(KeyCode.R))
        {
            angle1 = 0.09966f;
            angle2 = radianP;
        }
        
        offset.x = (Mathf.Cos(angle1) * distance) * Mathf.Sin(angle2);
        offset.y = Mathf.Sin(angle1) * distance;
        offset.z = (Mathf.Cos(angle1) * distance) * Mathf.Cos(angle2);

        transform.position = lookPos + offset;
        Vector3 offsetQ = lookPos - transform.position;
        Quaternion rotationQ = Quaternion.LookRotation(offsetQ);
        transform.rotation = rotationQ;
    }
}
