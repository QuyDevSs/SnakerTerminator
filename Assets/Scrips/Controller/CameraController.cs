using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    void Update()
    {
        if (CreatePlayer.Instance == null)
        {
            
            return;
        }
        transform.position = CreatePlayer.Instance.transform.position + new Vector3(0, 0, -10); 
    }
}
