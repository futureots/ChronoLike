using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class KeyWord
{
    public List<Character> targets;
    public abstract void Activate(GameManager inManager,Character inCaster);
    public virtual List<string> GetVariables(Character caster, Character target = null)
    {
        return null;
    }
    public virtual void SetTarget(List<Character> inTargets)
    {
        targets = inTargets;
    }
    public virtual void SetTarget(Character inTargets)
    {
        targets = new() { inTargets};
    }
}
