using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterCreator : MonoBehaviour
{
    public Sprite image;
    public CharacterLoader loader;
    void Start()
    {
        CharacterData characterData = CreateCharacterData();

        string data = CharacterData.SerializeCardData(characterData);                                                         //데이터를 json 스트링으로 변환
        string path = Path.Combine(Application.dataPath + "/Data/Characters", characterData.name + ".json");        //json파일을 위치, 파일 이름으로 제작
        Debug.Log(data);
        Debug.Log(path);
        File.WriteAllText(path, data);
        loader.characterName = characterData.name;
        loader.LoadCharacter();
    }

    public CharacterData CreateCharacterData()
    {
        CharacterData characterData = new CharacterData();


        characterData.name = "Ifrit";
        characterData.type = ColorType.Red;
        characterData.artPath = SpriteConverter.GetSpritePath(image);

        characterData.statusList = new();

        Status atk = new("Atk", 8, 999, 0);
        characterData.statusList.Add(atk);

        Status hp = new("Hp", 27, 999, 0);
        characterData.statusList.Add(hp);

        Status def = new("Def", 7, 999, 0);
        characterData.statusList.Add(def);



        characterData.characterAbility = new();
        TRandom random = new(1, 1);
        DealAndHeal keyWord = new(true, 1);
        CharacterAbility ab = new(keyWord,random,CharacterAbility.AbilityType.TurnStart);

        characterData.characterAbility.Add(ab);

        return characterData;
    }
}
/*
 *
        characterData.name = "Ifrit";
        characterData.type = ColorType.Red;
        characterData.artPath = SpriteConverter.GetSpritePath(image);

        characterData.statusList = new();

        Status atk = new("Atk", 8, 999, 0);
        characterData.statusList.Add(atk);

        Status hp = new("Hp", 27, 999, 0);
        characterData.statusList.Add(hp);

        Status def = new("Def", 7, 999, 0);
        characterData.statusList.Add(def);

        characterData.characterAbility = new();

        TRandom random = new(1, 1);
        DealAndHeal keyWord = new(random, true, 1);
        Ability ab = new(keyWord);

        characterData.characterAbility.Add("TurnStart",ab);

 * 
 * characterData.name = "Undine";
        characterData.type = ColorType.Blue;
        characterData.artPath = SpriteConverter.GetSpritePath(image);

        characterData.statusList = new();

        Status atk = new("Atk", 5, 999, 0);
        characterData.statusList.Add(atk);

        Status hp = new("Hp", 30, 999, 0);
        characterData.statusList.Add(hp);

        Status def = new("Def", 10, 999, 0);
        characterData.statusList.Add(def);

        characterData.characterAbility = new();

        TRandom random = new(1, 1);
        Poison dot = new();
        AddBuff addBuff = new(dot, 1);
        Ability ab = new(addBuff,random);

        characterData.characterAbility.Add("TurnStart",ab);


 * characterData.name = "Alps";
        characterData.type = ColorType.Green;
        characterData.artPath = SpriteConverter.GetSpritePath(image);

        characterData.statusList = new();

        Status atk = new("Atk", 6, 999, 0);
        characterData.statusList.Add(atk);

        Status hp = new("Hp", 29, 999, 0);
        characterData.statusList.Add(hp);

        Status def = new("Def", 9, 999, 0);
        characterData.statusList.Add(def);

        characterData.characterAbility = new();
        EditCost editCost = new(ColorType.Green, true);
        CharacterAbility ab = new();
        ab.keyWord = editCost;

        characterData.characterAbility.Add(ab);
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */