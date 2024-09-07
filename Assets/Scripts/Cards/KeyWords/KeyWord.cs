using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class KeyWord
{
    public CharacterViz caster;
    public virtual void SetKeyword(CharacterViz inCaster)
    {
        caster = inCaster;
    }
    public virtual void Init() 
    {
        //Debug.Log(GetType() + "Init"); 
    }
    public virtual void Delete() { Debug.Log(GetType() + "Init"); }
    public abstract void Active(CardViz cardViz,CharacterViz target);
    public virtual void Active(int value ,CardViz cardViz, CharacterViz target) { }
    public virtual List<string> GetVariables(CharacterViz target = null)
    {
        return null;
    }
}
