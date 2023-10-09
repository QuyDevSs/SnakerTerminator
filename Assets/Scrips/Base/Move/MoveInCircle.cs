using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInCircle : MonoBehaviour
{
    public float radius = 5.0f;
    public float speed = 2.0f;

    private float angle = 0.0f;
    private Vector2 centerPosition;

    void Start()
    {
        centerPosition = transform.position;
    }

    void Update()
    {
        angle += speed * Time.deltaTime;

        float x = centerPosition.x + radius * Mathf.Cos(angle);
        float y = centerPosition.y + radius * Mathf.Sin(angle);

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
