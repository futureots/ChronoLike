using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhanceValue : ValueKeyWord
{
    public ValueKeyWord keyWord;

    public EnhanceValue() : this(null, 0) { }
    public EnhanceValue(ValueKeyWord inKeyWord,int inBasicValue, float inCoef=0, string inStatusName=null)
    {
        keyWord = inKeyWord;
        basicValue = inBasicValue;
        coef = inCoef;
        statusName = inStatusName;
    }
    public EnhanceValue(ValueKeyWord inKeyWord,float inCoef, string inStatusName) : this(inKeyWord,0, inCoef, inStatusName) { }

    public override void Activate(CharacterViz caster, CharacterViz target)
    {
        keyWord.Activate(caster, target);

        EnhanceKeyWord();
    }

    public override List<string> GetVariables(CharacterViz caster, CharacterViz target = null)
    {
        List<string> variables = new List<string>();
        if (caster != null)
        {
            string str = "";
            if (basicValue != 0)
            {
                str += basicValue.ToString();
            }
            if (statusName != null)
            {
                if (basicValue != 0) str += " + ";
                Status stat = Status.GetStatus(caster.statusList, statusName);
                int damage = (int)(coef * stat.value);
                str += damage.ToString();
            }
            variables.Add(str);
        }
        
        variables.AddRange(keyWord.GetVariables(caster, target));
        //Debug.Log(variables.Count);
        return variables;
    }

    public void EnhanceKeyWord()
    {
        if (keyWord == null) return;
        keyWord.basicValue += basicValue;
        keyWord.coef += coef;
    }
}
