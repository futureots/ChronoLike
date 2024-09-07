using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhanceValue : KeyWordInKeyWord
{
    public int basicValue;
    public float coef;
    public EnhanceValue() : this(null, 0) { }
    public EnhanceValue(ValueKeyWord inKeyWord,int inBasicValue, float inCoef=0)
    {
        keyWord = inKeyWord;
        basicValue = inBasicValue;
        coef = inCoef;
    }
    public EnhanceValue(ValueKeyWord inKeyWord,float inCoef) : this(inKeyWord,0, inCoef) { }

    public override void Active(CardViz cardViz, CharacterViz target)
    {
        keyWord.Active(cardViz,target);

        EnhanceKeyWord();
    }

    public override List<string> GetVariables(CharacterViz target = null)
    {
        List<string> variables = new List<string>();
        if (caster != null)
        {
            string str = "";
            int damage = basicValue;
            ValueKeyWord valueKeyWord = keyWord as ValueKeyWord;
            if(valueKeyWord != null)
            {
                if(valueKeyWord.statusName != null)
                {
                    Status stat = Status.GetStatus(caster.statusList, valueKeyWord.statusName);
                    damage += (int)(stat.value*coef);
                }
            }
            str = damage.ToString();
            variables.Add(str);
        }
        
        variables.AddRange(keyWord.GetVariables(target));
        //Debug.Log(variables.Count);
        return variables;
    }

    public void EnhanceKeyWord()
    {
        if (keyWord == null) return;
        ValueKeyWord valueKeyWord = keyWord as ValueKeyWord;
        if (valueKeyWord == null) return;
        valueKeyWord.basicValue += basicValue;
        valueKeyWord.coef += coef;
    }
}
