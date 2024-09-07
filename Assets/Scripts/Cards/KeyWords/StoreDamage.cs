using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreDamage : ValueKeyWord
{
    public int storeValue = 0;
    public override List<string> GetVariables(CharacterViz target = null)
    {
        List<string> variables = new List<string>();
        variables.Add(storeValue.ToString());
        return variables;
    }
    public override void Active(CardViz cardViz, CharacterViz target)
    {
        //target.damaged(storevalue);
    }
    public override void Init()
    {
        //caster.delegate+ {storevalue += damage;}
    }
    public override void Delete()
    {
        //caster.delegate- {storevalue += damage;}
    }
}
