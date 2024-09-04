using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class KeyWord
{
    public virtual void Init()
    {

    }
    public abstract void Activate(CharacterViz caster,CharacterViz target);
    public virtual void Activate(int value , CharacterViz target) { }
    public virtual List<string> GetVariables(CharacterViz caster, CharacterViz target = null)
    {
        return null;
    }
}
