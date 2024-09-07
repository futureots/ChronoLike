using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWound : KeyWordInKeyWord
{
    public bool isHpFull;
    public CheckWound() : this(null) { }
    public CheckWound(KeyWord inKeyWord,bool inIsHpFull=false)
    {
        keyWord = inKeyWord;
        isHpFull = inIsHpFull;
    }
    public override void Active(CardViz cardViz, CharacterViz target)
    {
        bool isFull;
        List<Status> statuses = caster.statusList;
        isFull = (Status.GetStatus(statuses, "Hp").value == Status.GetStatus(statuses, "CurrentHp").value);
        //Debug.Log("Hp = " + Status.GetStatus(statuses, "Hp").value + "// CurrentHp = " + Status.GetStatus(statuses, "CurrentHp").value + " isFull = " + isFull);
        if(isFull == isHpFull)
        {
            keyWord.Active(cardViz,target);
        }
    }
    public override List<string> GetVariables(CharacterViz target = null)
    {
        List<string> variables = new List<string>();
        string str = "";
        if (isHpFull) str += "<b>건강</b>";
        else str += "<b>상처</b>"; 
        variables.Add(str);
        variables.AddRange(keyWord.GetVariables(target));
        return variables;
    }
}
