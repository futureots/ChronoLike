using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class RelicData
{
    public string name;
    public string imagePath;
    public List<CharacterAbility> ability;


    static JsonSerializerSettings serializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
    public static RelicData DeserializeCardData(string inString)
    {
        if (inString == null) return null;
        return JsonConvert.DeserializeObject<RelicData>(inString, serializerSettings);
    }
    public static string SerializeCardData(RelicData inCardData)
    {
        if (inCardData == null) return null;
        return JsonConvert.SerializeObject(inCardData, serializerSettings);
    }
}
