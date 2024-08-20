using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TTarget : TargetType
{
    public override List<CharacterViz> GetTarget(bool casterTeam)
    {
        if(GameManager.currentManager.characterManager.currentTarget == null) return null;
        List<CharacterViz> characters = new();
        characters.Add(GameManager.currentManager.characterManager.currentTarget);
        return characters;
    }
}
