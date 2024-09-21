using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class MapData
{
    public KeyValuePair<int, int> curNode;
    public List<List<MapNode>> map;
    

    static JsonSerializerSettings serializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
    public static MapData DeserializeCardData(string inString)
    {
        if (inString == null) return null;
        return JsonConvert.DeserializeObject<MapData>(inString, serializerSettings);
    }
    public static string SerializeCardData(MapData inCardData)
    {
        if (inCardData == null) return null;
        return JsonConvert.SerializeObject(inCardData, serializerSettings);
    }
}
