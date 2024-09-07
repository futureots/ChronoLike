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
    public override void Active(CardViz cardViz, CharacterViz target)
    {
        if (target == null) return;
        
        int damage = basicValue;
        if (statusName != null)
        {
            Status status = Status.GetStatus(caster.statusList, statusName);
            damage += (int)(coef * status.value);
        }
        Debug.Log("damage = "+ damage);
        if (isDeal) target.Damaged(damage);
        else target.Damaged(-damage);
    }
    public override void Active(int value, CardViz cardViz, CharacterViz target)
    {
        if (target == null) return;
        if (isDeal) target.Damaged(value);
        else target.Damaged(-value);
    }
    public override List<string> GetVariables(CharacterViz target = null)
    {
        if (caster == null) return new List<string> { "" };
        

        string str = "";
        int value = basicValue;
        if (statusName != null)
        {
            Status stat = Status.GetStatus(caster.statusList, statusName);
            if (stat != null)
            {
                int damage = (int)(coef * stat.value);
                value += damage;
            }
        }
        str += value;

        List<string> result = new List<string>();
        result.Add(str);
        return result;
    }
}
