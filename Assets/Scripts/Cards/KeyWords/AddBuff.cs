using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuff : ValueKeyWord
{
    public Buff buff;
    public AddBuff() : this(null,0) { }
    public AddBuff(Buff inBuff, int inBasicValue=0, float inCoef = 0, string inStatusName = null)
    {
        buff = inBuff;
        basicValue = inBasicValue;
        coef = inCoef;
        statusName = inStatusName;
    }
    public AddBuff(Buff inBuff, float inCoef = 0, string inStatusName = null) : this(inBuff, 0, inCoef, inStatusName) { }
    public override void Activate(CharacterViz caster, CardViz card, CharacterViz target)
    {
        if (target == null) return;
        buff.countNum = basicValue;
        if (statusName != null)
        {
            Status status = Status.GetStatus(caster.statusList, statusName);
            buff.countNum+= (int)(coef * status.value);
        }


        target.AttachBuff(buff.Clone());
    }
    public override List<string> GetVariables(CharacterViz caster, CharacterViz target = null)
    {
        if (caster == null) return new List<string> { "" };

        string str = "";
        if (basicValue != 0)
        {
            str += basicValue.ToString();
        }
        if (statusName != null)
        {
            if (basicValue != 0) str += " + ";
            Status stat = Status.GetStatus(caster.statusList, statusName);
            if (stat != null)
            {
                int damage = (int)(coef * stat.value);
                str += damage.ToString();
            }
        }


        List<string> result = new List<string>();
        result.Add(str);
        return result;
    }

}
