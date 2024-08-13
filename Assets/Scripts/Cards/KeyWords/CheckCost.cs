using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCost : KeyWord
{
    public ColorType color;
    public int costNum;
    public bool isMax;
    public KeyWord keyWord;

    public CheckCost() : this(null, ColorType.Red) { }
    public CheckCost(KeyWord inKeyWord,ColorType inColor,int inCostNum=0, bool inIsMax = false) 
    {
        keyWord = inKeyWord;
        color = inColor;
        costNum = inCostNum;
        isMax = inIsMax;
        
    }


    public override void Activate(GameManager inManager, Character inCaster)
    {
        int inCostNum;
        if (isMax)
        {
            inCostNum = Status.GetStatus(inManager.costManager.costList[(int)color].costStatus, "MaxCost").value;
        }
        else
        {
            inCostNum = Status.GetStatus(inManager.costManager.costList[(int)color].costStatus, "CurrentCost").value;
        }
        if (inCostNum >= costNum)
        {
            keyWord.Activate(inManager,inCaster);
        }

    }
    public override List<string> GetVariables(Character caster, Character target = null)
    {
        
        List<string> variable = new List<string>();
        string str = "";
        if (isMax) str += "<b>거목</b>";
        else str += "<b>열매</b>";
        str += costNum.ToString();
        variable.Add(str);
        variable.AddRange(keyWord.GetVariables(caster,target));
        return variable;
    }
    public override void SetTarget(List<Character> inTargets)
    {
        keyWord.SetTarget(inTargets);
    }
    public override void SetTarget(Character inTargets)
    {
        keyWord.SetTarget(inTargets);
    }
}
