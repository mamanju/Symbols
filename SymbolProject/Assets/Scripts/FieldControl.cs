using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldControl : MonoBehaviour
{
    [SerializeField]
    private int _pallCount = 10;
    [SerializeField]
    private GameObject _parent;
    [SerializeField]
    private GameObject _pollPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < _pallCount; i++)
        {
            var obj = Instantiate(_pollPrefab, _parent.transform);
            var x = Mathf.Sin((360.0f / (float)_pallCount) * (float)i * Mathf.PI / 180) * 37f;
            var y = Mathf.Cos((360.0f / (float)_pallCount) * (float)i * Mathf.PI / 180) * 37f;
            obj.transform.localPosition = new Vector3(x, 5, y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
