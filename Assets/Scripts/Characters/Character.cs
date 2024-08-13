using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character
{
    public CharacterData data;
    public string name;
    public Sprite art;
    public ColorType type;
    public List<Status> statusList;
    public Dictionary<string,Ability> characterAbility;
    public List<Buff> buffs;
    public bool isAlly;                                                              //아군인지 적군인지
    public delegate void UseCardAfter();
    public UseCardAfter useCardAfter = new(() => { });
    public Character(CharacterData characterData, bool isAlly)
    {
        buffs = new();
        statusList = new();
        LoadCharacterData(characterData, isAlly);
        
    }
    public void LoadCharacterData(CharacterData characterData, bool isAlly)
    {
        if (characterData == null) return;
        data = characterData;
        name = characterData.name;
        type = characterData.type;
        art = SpriteConverter.LoadSpriteFile(characterData.artPath);
        this.isAlly = isAlly;
        foreach (var item in characterData.statusList)
        {
            Status status = new(item);
            statusList.Add(status);
            if (item.name.Equals("Hp"))
            {
                Status currentHp = new("CurrentHp", item.value, item.value, item.min);
                statusList.Add(currentHp);
            }
        }
        characterAbility = characterData.characterAbility;

    }

    public void EditCharacter(string name, int value, Status.Operation operation, int code = 0)//code = 1: max, 2 :min, 그 외 : value
    {
        foreach (var item in statusList)
        {
            if (item.name.Equals(name))
            {
                item.EditValue(value, operation, code);
            }
        }
    }

    public bool StatIsZero(string statusName)
    {
        Status status = Status.GetStatus(statusList, statusName);
        if (status == null) return false;
        if (status.value == 0) return true;
        return false;
    }

    

    public void AttachBuff(Buff inBuff)
    {
        foreach (var item in buffs)
        {
            if (item.MergeBuff(inBuff))
            {
                return;
            }
            
        }
        inBuff.AttachBuff(this);
    }
}

