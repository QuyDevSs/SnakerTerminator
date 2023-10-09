using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTranformUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(Vector3.forward, 90);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.up);
    }
}
