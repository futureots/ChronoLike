using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TTarget : TargetType
{
    public override List<Character> GetTarget(bool casterTeam)
    {
        if(GameManager.currentManager.characterManager.currentTarget == null) return null;
        List<Character> characters = new();
        characters.Add(GameManager.currentManager.characterManager.currentTarget);
        return characters;
    }
}
