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
    public override void Activate(CharacterViz caster,CardViz card, CharacterViz target)
    {
        int colorNum = (int)color;
        string targetCost;
        if (isMaxCost)
        {
            targetCost = "MaxCost";
        }
        else
        {
            targetCost = "CurrentCost";
        }
        Status maxCost = Status.GetStatus(GameManager.currentManager.costManager.costList[colorNum].costStatus, targetCost);
        maxCost.EditValue(1, Status.Operation.Add);
    }
    public override List<string> GetVariables(CharacterViz caster, CharacterViz target = null)
    {
        if (caster == null) return new List<string> { "" };
        string str = "1";
        List<string> result = new List<string>();
        result.Add(str);
        return result;
    }
}
