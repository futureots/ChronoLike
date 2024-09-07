using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCost : KeyWordInKeyWord
{
    public ColorType color;
    public int costNum;
    public bool isMax;

    public CheckCost() : this(null, ColorType.Red) { }
    public CheckCost(KeyWord inKeyWord,ColorType inColor,int inCostNum=0, bool inIsMax = false) 
    {
        keyWord = inKeyWord;
        color = inColor;
        costNum = inCostNum;
        isMax = inIsMax;
        
    }
    public override void Active(CardViz cardViz, CharacterViz target)
    {
        int inCostNum;
        if (isMax)
        {
            inCostNum = Status.GetStatus(GameManager.currentManager.costManager.costList[(int)color].costStatus, "MaxCost").value;
        }
        else
        {
            inCostNum = Status.GetStatus(GameManager.currentManager.costManager.costList[(int)color].costStatus, "CurrentCost").value;
        }
        if (inCostNum >= costNum)
        {
            keyWord.Active(cardViz, target);
        }

    }
    public override List<string> GetVariables(CharacterViz target = null)
    {
        
        List<string> variable = new List<string>();
        string str = "";
        if (isMax) str += "<b>거목</b>";
        else str += "<b>열매</b>";
        str += costNum.ToString();
        variable.Add(str);
        variable.AddRange(keyWord.GetVariables(target));
        return variable;
    }

}
