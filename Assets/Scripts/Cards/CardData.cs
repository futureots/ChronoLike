using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;


public class CardData
{
    
    public string title;
    public string imagePath;
    public string abilityDescription;


    public bool isNeedTarget;
    public ColorType cardColor;
    public Dictionary<ColorType, int> costs;
    public List<CardCost> costs1;
    public List<CardAbility> cardAbility;


    static JsonSerializerSettings serializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
    public static CardData DeserializeCardData(string inString)
    {
        if (inString == null) return null;
        return JsonConvert.DeserializeObject<CardData>(inString,serializerSettings);
    }
    public static string SerializeCardData(CardData inCardData)
    {
        if (inCardData == null) return null;
        return JsonConvert.SerializeObject(inCardData, serializerSettings);
    }
}
