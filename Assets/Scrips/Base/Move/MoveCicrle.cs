using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCicrle : MonoBehaviour, ITypeMove
{
    [SerializeField]
    public float radius;
    Vector3 startPos;
    Vector3 direction = Vector3.zero;
    public Vector3 Move()
    {
        return direction;
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        direction = Vector3.zero;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 radialVector = transform.position - startPos;
        float distance = Mathf.Clamp(radialVector.magnitude, -0, radius);
        Vector3 vec3 = radialVector.normalized * distance;
        if (distance >= radius)
        {
            direction = new Vector3(-radialVector.y, radialVector.x, 0);
        }
    }
}
