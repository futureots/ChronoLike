using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    public CharacterViz owner;
    public TargetType targetType;
    public List<KeyWord> keywords;

    public void Execute()
    {
        List<CharacterViz> targets;
        targets = targetType.GetTarget(owner.isAlly);
        foreach (var keyword in keywords)
        {
            
        }
    }
}
