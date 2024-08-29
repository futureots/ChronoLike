using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainShield : ValueKeyWord
{

    public GainShield() : this(0) { }
    public GainShield(int inBasicValue, string inStatusName ="", float inCoef=0)
    {
        basicValue = inBasicValue;
        coef = inCoef;
        statusName = inStatusName;
    }


    public override void Activate(CharacterViz caster, CharacterViz target)
    {
        if (target == null) return;
        int value = basicValue;
        if (statusName != null)
        {
            Status stat = Status.GetStatus(caster.statusList, statusName);
            if (stat != null)
            {
                value += (int)(stat.value * coef);
            }
        }
        Status charShield = Status.GetStatus(target.statusList, "Shield");
        if(charShield == null)
        {
            Status shield = new Status("Shield", value);
            Debug.Log("shield Gauge = " + shield.value);
            target.statusList.Add(shield);
        }
        else
        {
            charShield.EditValue(value, Status.Operation.Add);
        }
    }
    public override List<string> GetVariables(CharacterViz caster, CharacterViz target = null)
    {
        List<string> variables = new List<string>();
        if (caster == null)
        {
            variables.Add("0");
            return variables;
        }
        int value = basicValue;
        if (statusName != null)
        {
            Status stat = Status.GetStatus(caster.statusList, statusName);
            if(stat != null)
            {
                value += (int)(stat.value * coef);
            }
        }
        variables.Add(value.ToString());
        return variables;
        
    }
}
