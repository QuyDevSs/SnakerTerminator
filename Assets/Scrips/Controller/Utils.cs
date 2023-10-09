using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.Reflection;

public class SubInfo
{
    public string assemblyName;
    public string type;
    public string typeInfo;
}
public class SubEffectInfo
{
    public Type type;
    public object data;
}

static public class Utils
{
    public static void GenerateMeshPolygon2D(MeshFilter meshFilter, PolygonCollider2D polygon)
    {
        Mesh mesh = new Mesh();
        Vector2[] colliderPoints = polygon.points;
        Vector3[] meshVertices = new Vector3[colliderPoints.Length];
        for (int i = 0; i < colliderPoints.Length; i++)
        {
            meshVertices[i] = new Vector3(colliderPoints[i].x, colliderPoints[i].y, 0f);
        }
        mesh.vertices = meshVertices;

        int vertexIndex = 0;
        int[] meshTriangles = new int[(meshVertices.Length - 2) * 6];
        for (int i = 0; i < meshTriangles.Length; i += 6)
        {
            meshTriangles[i] = 0;
            meshTriangles[i + 1] = vertexIndex + 1;
            meshTriangles[i + 2] = vertexIndex + 2;
            meshTriangles[i + 3] = 0;
            meshTriangles[i + 4] = vertexIndex + 2;
            meshTriangles[i + 5] = vertexIndex + 1;

            vertexIndex++;
        }
        mesh.triangles = meshTriangles;

        Vector2[] meshUv = new Vector2[colliderPoints.Length];

        for (int i = 0; i < meshVertices.Length; i++)
        {
            Vector3 uvPosition = meshVertices[i];
            meshUv[i].x = (uvPosition.x - mesh.bounds.min.x) / mesh.bounds.size.x;
            meshUv[i].y = (uvPosition.y - mesh.bounds.min.y) / mesh.bounds.size.y;
        }
        mesh.uv = meshUv;

        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }
    public static void GenerateMeshByPoints(MeshFilter meshFilter, Vector3[] points)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = points;

        int vertexIndex = 0;
        int[] meshTriangles = new int[(points.Length - 2) * 6];
        for (int i = 0; i < meshTriangles.Length; i += 6)
        {
            meshTriangles[i] = 0;
            meshTriangles[i + 1] = vertexIndex + 1;
            meshTriangles[i + 2] = vertexIndex + 2;
            meshTriangles[i + 3] = 0;
            meshTriangles[i + 4] = vertexIndex + 2;
            meshTriangles[i + 5] = vertexIndex + 1;

            vertexIndex++;
        }
        mesh.triangles = meshTriangles;

        Vector2[] meshUv = new Vector2[points.Length];

        for (int i = 0; i < points.Length; i++)
        {
            Vector3 uvPosition = points[i];
            meshUv[i].x = (uvPosition.x - mesh.bounds.min.x) / mesh.bounds.size.x;
            meshUv[i].y = (uvPosition.y - mesh.bounds.min.y) / mesh.bounds.size.y;
        }
        mesh.uv = meshUv;

        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }
    public static SubEffectInfo GetSubEffectInfo(JSONNode jsonNode)
    {
        SubInfo subInfo = JsonUtility.FromJson<SubInfo>(jsonNode.ToString());
        SubEffectInfo subEffectInfo = new SubEffectInfo();
        Assembly assembly = Assembly.Load(subInfo.assemblyName);
        subEffectInfo.type = assembly.GetType(subInfo.type);
        subEffectInfo.data = JsonUtility.FromJson(jsonNode["data"].ToString(), assembly.GetType(subInfo.typeInfo));
        return subEffectInfo;
    }
    public static SubEffectInfo[] GetSubEffectInfos(JSONArray array)
    {
        List<SubEffectInfo> listsubEffectInfo = new List<SubEffectInfo>();
        for (int i = 0; i < array.Count; i++)
        {
            JSONNode jsonNode = array[i];
            SubEffectInfo subEffectInfo = GetSubEffectInfo(jsonNode);
            listsubEffectInfo.Add(subEffectInfo);
        }
        return listsubEffectInfo.ToArray();
    }
}
