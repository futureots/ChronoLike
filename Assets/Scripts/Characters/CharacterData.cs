using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;



public class CharacterData
{
    public string name;
    public ColorType type;
    
    public List<Status> statusList;
    public Dictionary<string,Ability> characterAbility;

    public string artPath;                       //캐릭터 이미지 주소값

    static JsonSerializerSettings serializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
    public static CharacterData DeserializeCardData(string inString)
    {
        if (inString == null) return null;
        return JsonConvert.DeserializeObject<CharacterData>(inString, serializerSettings);
    }
    public static string SerializeCardData(CharacterData inCardData)
    {
        if (inCardData == null) return null;
        return JsonConvert.SerializeObject(inCardData, serializerSettings);
    }



}