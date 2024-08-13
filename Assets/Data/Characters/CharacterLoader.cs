using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterLoader : MonoBehaviour
{
    public string characterName;
    public CharacterViz characterViz;
    public CharacterData characterData;
    public void LoadCharacter()
    {
        string path = Path.Combine(Application.dataPath + "/Data/Characters", characterName + ".json");
        string data = null;
        if (File.Exists(path))
        {
            data = File.ReadAllText(path);
        }


        characterData = CharacterData.DeserializeCardData(data);
        Character temp = new(characterData,true);
        characterViz.LoadCharacter(temp);
    }
}
