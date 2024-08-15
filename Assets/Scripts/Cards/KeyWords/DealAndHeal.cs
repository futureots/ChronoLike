using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DealAndHeal : ValueKeyWord
{
    public bool isDeal;
    
    public DealAndHeal() : this(true, 0) { }
    public DealAndHeal(bool isDeal,int inBasicValue,float inCoef, string inStatusName)
    {
        this.isDeal = isDeal;
        basicValue = inBasicValue;
        coef = inCoef;
        statusName = inStatusName;
    }
    public DealAndHeal(bool isDeal, float inCoef, string inStatusName) : this(isDeal, 0, inCoef, inStatusName) { }
    public DealAndHeal(bool isDeal, int inBasicValue) : this(isDeal, inBasicValue,0,null) { }
    public override void Activate(Character caster, Card card, Character target)
    {
        if (target == null) return;
        
        int damage = basicValue;
        if (statusName != null)
        {
            Status status = Status.GetStatus(caster.statusList, statusName);
            damage += (int)(coef * status.value);
        }
        Debug.Log("damage = "+ damage);
        if (isDeal) target.EditCharacter("CurrentHp", -damage, Status.Operation.Add);
        else target.EditCharacter("CurrentHp", damage, Status.Operation.Add);
    }
    public override List<string> GetVariables(Character caster, Character target = null)
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
