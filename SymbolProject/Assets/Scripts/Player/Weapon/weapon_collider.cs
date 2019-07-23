using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_collider : MonoBehaviour
{
    [SerializeField]
    private GameObject wrist;

    private GameObject[] weapons = new GameObject[10];

    private BoxCollider[] boxColliders = new BoxCollider[10];

    private bool collider_Flag = false;

    private float colliderTime;

    [SerializeField]
    private float max_colliderTime = 0.5f;

    private int num;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i >= 3) { return; }
            weapons[i] = wrist.transform.GetChild(i).gameObject;
            boxColliders[i] = weapons[i].GetComponent<BoxCollider>();
        }

        colliderTime = max_colliderTime;

    }

    private void Update()
    {
        if (collider_Flag == false) { return; }
        colliderTime -= Time.deltaTime;
        if (colliderTime <= 0)
        {
            OffCollider(num);
            collider_Flag = false;
            colliderTime = max_colliderTime;
        }
    }


    public void OnCollider(int _num)
    {
        num = _num;
        if (_num >= 3) { return; }
        boxColliders[_num].enabled = true;
        collider_Flag = true;
    }

    public void OffCollider(int _num)
    {
        if (_num >= 3) { return; }
        boxColliders[_num].enabled = false;
    }
}
