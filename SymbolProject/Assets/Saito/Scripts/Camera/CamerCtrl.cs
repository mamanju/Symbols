using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerCtrl : MonoBehaviour
{
    private float angle1;
    private float angle2;

    private float distance;
    private float distanceT;

    private bool rayJudg;

    [SerializeField]
    private Transform player;
    private Vector3 offset;

    [SerializeField]
    private float speed = 1.0f;
    private float radianP;

    private bool cameraUp;
    private bool cameraDown;

    private Vector3 lookPos;

    // Usve this for initialization
    void Start()
    {
        lookPos = player.position + new Vector3(0, 0.3f, 0);

        offset = transform.position - lookPos;


        distance = Vector3.Distance(lookPos, transform.position);

        angle1 = Mathf.Acos(Vector3.Dot(player.forward * -1, offset.normalized));
        angle2 = 3.14174f;

        rayJudg = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        lookPos = player.position + new Vector3(0, 1, 0);

        radianP = player.eulerAngles.y * (Mathf.PI / 180.0f) + 3.14174f;

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

        //右に回転
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angle2 += Time.deltaTime * speed;
            if (angle2 > 6.28319) { angle2 -= 6.28319f; };
        }
        //左に回転
        if (Input.GetKey(KeyCode.RightArrow))
        {
            angle2 -= Time.deltaTime * speed;
            if (angle2 <= 0) { angle2 += 6.28319f; };
        }
        //上に移動
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (angle1 > 1.3f && cameraUp == true) { angle1 = 1.3f; cameraDown = false; }
            else if (angle1 < 1.3f) { angle1 += Time.deltaTime * speed; cameraUp = true; }
        }
        //下に移動
        if (Input.GetKey(KeyCode.DownArrow))
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
        transform.position = lookPos + offset;
        Vector3 offsetQ = lookPos - transform.position;
        Quaternion rotationQ = Quaternion.LookRotation(offsetQ);
        transform.rotation = rotationQ;
        player.rotation = rotationQ;
    }
}
