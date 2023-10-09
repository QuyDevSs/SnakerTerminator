using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class EntityVO : BaseVO
{
    public EntityInfo GetEntityInfo(int level)
    {
        JSONArray array = data.AsArray;
        if (level >= array.Count)
        {
            return JsonUtility.FromJson<EntityInfo>(array[array.Count - 1].ToString());
        }
        return JsonUtility.FromJson<EntityInfo>(array[level - 1].ToString());
    }
}
