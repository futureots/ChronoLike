using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EditCost : KeyWord
{
    public ColorType color;
    public bool isMaxCost;
    public int value;
    public EditCost(ColorType inColor, bool inIsMaxCost , int inValue = 1)
    {
        color = inColor;
        isMaxCost = inIsMaxCost;
        value = inValue;
    }
    public override void Activate(CharacterViz caster, CharacterViz target)
    {
        int colorNum = (int)color;
        CostElement costElement = GameManager.currentManager.costManager.costList[colorNum];
        costElement.EditCost(value, isMaxCost);
    }
    public override List<string> GetVariables(CharacterViz caster, CharacterViz target = null)
    {
        string str = value.ToString();
        List<string> result = new List<string>();
        result.Add(str);
        return result;
    }
}
