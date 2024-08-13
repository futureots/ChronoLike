using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EditCost : KeyWord
{
    public ColorType color;
    public bool isMaxCost;
    public EditCost(ColorType inColor, bool inIsMaxCost)
    {
        color = inColor;
        isMaxCost = inIsMaxCost;
    }
    public override void Activate(GameManager inManager,Character inCaster)
    {
        int colorNum = (int)color;
        string target;
        if (isMaxCost)
        {
            target = "MaxCost";
        }
        else
        {
            target = "CurrentCost";
        }
        Status maxCost = Status.GetStatus(inManager.costManager.costList[colorNum].costStatus, target);
        maxCost.EditValue(1, Status.Operation.Add);
        inManager.costManager.RenderCost(colorNum);
    }
    public override List<string> GetVariables(Character caster, Character target = null)
    {
        if (caster == null) return new List<string> { "" };
        string str = "1";
        List<string> result = new List<string>();
        result.Add(str);
        return result;
    }
}
