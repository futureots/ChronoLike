using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbility
{
    public KeyWord keyWord;
    public enum AbilityType
    {
        GameStart=0,
        TurnStart=1,
        TurnEnd=2,
        ActionBefore=3,
        ActionAfter=4,
        Damaged=5,
        Dead=6
    }
    public AbilityType actionType;
    public TargetType targetType;
    public CharacterViz owner;

    public CharacterAbility() : this(null) { }
    public CharacterAbility(KeyWord inKeyWord, TargetType inTargetType = null, AbilityType abilityType = 0)
    {
        keyWord = inKeyWord;
        targetType = inTargetType;
        actionType = abilityType;
        owner = null;
    }
    public void Initiate()
    {
        keyWord.Init();
    }
    public void Delete()
    {
        keyWord.Delete();
    }
    public void Activate()
    {
        List<CharacterViz> targets = new List<CharacterViz>();
        if(targetType == null)
        {
            targets.Add(owner);
        }
        else if(targetType is TTarget)
        {
            return;
        }
        else
        {
            targets.AddRange(targetType.GetTarget(owner.isAlly));
        }
        for(int i = 0; i < targets.Count; i++)
        {
            int atk = Status.GetStatus(owner.statusList, "Atk").value;
            keyWord.Active(null,targets[i]);
        }
    }
    
}
