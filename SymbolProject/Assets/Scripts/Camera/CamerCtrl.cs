using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Vector3 offset;

    private float distance;
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

    private void Awake()
    {
        transform.position = player.transform.forward * -8;
    }
    // Usve this for initialization
    void Start()
    {
        lookPos = player.transform.position + new Vector3(0, 1, 0);

        offset = transform.position - lookPos;


        distance = Vector3.Distance(lookPos, transform.position);

        angle1 = Mathf.Acos(Vector3.Dot(player.transform.forward * -1, offset.normalized));
        angle2 = 3.14174f;

        rayJudg = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lookPos = player.transform.position + new Vector3(0, 1, 0);
        
        radianP = player.transform.eulerAngles.y * (Mathf.PI / 180.0f) + 3.14174f;
        
        Ray ray = new Ray(lookPos, offset);
        RaycastHit hit;
        int distanceR = 10;
        //Rayの可視化
        Debug.DrawLine(ray.origin, ray.direction * distanceR, Color.red);
        
        if (rayJudg == false)
        {
            distanceT = distance;
            rayJudg = true;
        }
        if (Physics.Raycast(ray, out hit, distanceR))
        {
            if (rayJudg == true)
            {
                distance = Vector3.Distance(hit.point, lookPos);
            }
        }
        else
        {
            distance = distanceT;
            rayJudg = false;
        }

        float _horizontalR = Input.GetAxisRaw("Horizontal_R");
        float _verticalR = Input.GetAxisRaw("Vertical_R");
        if (_horizontalR != 0)
        {
            angle2 += Time.deltaTime * speed * _horizontalR;
            if (angle2 > 6.28319) { angle2 -= 6.28319f; };
            if (angle2 <= 0) { angle2 += 6.28319f; };
        }
        //上に移動
        if (_verticalR > 0)
        {
            if (angle1 > 1.3f && cameraUp == true) { angle1 = 1.3f; cameraDown = false; }
            else if (angle1 < 1.3f) { angle1 += Time.deltaTime * speed; cameraUp = true; }
        }
        //下に移動
        if (_verticalR < 0)
        {
            if (angle1 < 0.1f && cameraDown == true) { angle1 = 0.1f; cameraDown = false; }
            else if (angle1 >= 0.1f) { angle1 -= Time.deltaTime * speed; cameraUp = true; }
        }
        //ズームイン
        if (Input.GetKey(KeyCode.K))
        {
            if (distance <= 2) { distance = 2; }
            else if (distance > 2) { distance -= 0.1f; }
        }
        //ズームアウト
        if (Input.GetKey(KeyCode.L))
        {
            if (distance >= 7) { distance = 7; }
            else if (distance < 7) { distance += 0.1f; }
        }
        //リセット
        if (Input.GetKeyDown(KeyCode.P))
        {
            distance = distanceT;
            angle1 = 0.09966f;
            angle2 = radianP;
        }
        
        offset.x = (Mathf.Cos(angle1) * distance) * Mathf.Sin(angle2);
        offset.y = Mathf.Sin(angle1) * distance;
        offset.z = (Mathf.Cos(angle1) * distance) * Mathf.Cos(angle2);
        Vector3 offsetQ = lookPos - transform.position;
        Quaternion rotationQ = Quaternion.LookRotation(offsetQ);
        transform.rotation = rotationQ;
        transform.position = lookPos + offset;
    }
}
