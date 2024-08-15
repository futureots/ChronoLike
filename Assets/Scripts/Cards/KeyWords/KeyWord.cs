using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class KeyWord
{
    public abstract void Activate(Character caster, Card card, Character target);
    public virtual List<string> GetVariables(Character caster, Character target = null)
    {
        return null;
    }
}
