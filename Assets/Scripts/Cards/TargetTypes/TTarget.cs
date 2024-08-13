using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TTarget : TargetType
{
    public override List<Character> GetTarget(CharacterManager characterManager, bool casterTeam)
    {
        if(characterManager.currentTarget == null) return null;
        List<Character> characters = new();
        characters.Add(characterManager.currentTarget);
        return characters;
    }
}
