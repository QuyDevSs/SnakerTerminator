using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingScreen : MonoBehaviour
{
    void Update()
    {
        Vector2 normal = GetScreenEdgeNormal();
        Vector2 reflectedDirection = Vector2.Reflect(transform.up, normal);
        transform.up = reflectedDirection.normalized;
    }
    Vector2 GetScreenEdgeNormal()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPos.x <= 0 || screenPos.x >= Screen.width || screenPos.y <= 0 || screenPos.y >= Screen.height)
        {
            // Bạn đã tiếp cận cạnh của màn hình
            Vector2 normal = new Vector2(screenPos.x <= 0 ? -1 : (screenPos.x >= Screen.width ? 1 : 0),
                                         screenPos.y <= 0 ? -1 : (screenPos.y >= Screen.height ? 1 : 0));
            return normal;
        }

        return Vector2.zero;
    }

}
