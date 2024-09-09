using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;


public struct CharData
{
    public string characterName;
    public int characterLevel;
    public int currentHp;
    public List<string> characterDeck;
}
public class PlayData
{
    public List<CharData> partyData;
    


    public static void SaveData(PlayData GameData)
    {
        string data = SerializePlayData(GameData);
        string path = Path.Combine(Application.dataPath + "/Data/PlayData", "PlayData.json");        //json파일을 위치, 파일 이름으로 제작
        Debug.Log(data);
        Debug.Log(path);
        File.WriteAllText(path, data);
    }

    static JsonSerializerSettings serializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
    public static PlayData DeserializeCardData(string inString)
    {
        if (inString == null) return null;
        return JsonConvert.DeserializeObject<PlayData>(inString, serializerSettings);
    }
    public static string SerializePlayData(PlayData inPlayData)
    {
        if (inPlayData == null) return null;
        return JsonConvert.SerializeObject(inPlayData, serializerSettings);
    }
}
