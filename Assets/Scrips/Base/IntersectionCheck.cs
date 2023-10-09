using UnityEngine;
using LTAUnityBase.Base.DesignPattern;
public class IntersectionCheck : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public Transform pointD;

    //void Update()
    //{
    //    bool isIntersecting = IsIntersection(pointA.position, pointB.position, pointC.position, pointD.position);
    //    if (isIntersecting)
    //    {
    //        Debug.Log("AB intersects CD");
    //    }
    //    else
    //    {
    //        Debug.Log("AB does not intersect CD");
    //    }
    //}

    bool IsIntersection(Vector3 A, Vector3 B, Vector3 C, Vector3 D)
    {
        float denominator = ((D.y - C.y) * (B.x - A.x)) - ((D.x - C.x) * (B.y - A.y));
        float numerator1 = ((D.x - C.x) * (A.y - C.y)) - ((D.y - C.y) * (A.x - C.x));
        float numerator2 = ((B.x - A.x) * (A.y - C.y)) - ((B.y - A.y) * (A.x - C.x));

        if (denominator == 0)
        {
            return numerator1 == 0 && numerator2 == 0;
        }

        float r = numerator1 / denominator;
        float s = numerator2 / denominator;

        return (r >= 0 && r <= 1) && (s >= 0 && s <= 1);
    }
}
