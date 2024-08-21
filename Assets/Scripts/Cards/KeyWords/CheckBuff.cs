using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBuff : KeyWord
{
    public ValueKeyWord keyWord;
    public System.Type type;
    public bool isDisposable;
    public CheckBuff() : this(null) { }
    public CheckBuff(ValueKeyWord inKeyWord, bool inIsDisposable=true,System.Type inType = null)
    {
        keyWord = inKeyWord;
        isDisposable = inIsDisposable;
        type = inType;
    }
    


    public override void Activate(CharacterViz caster,CardViz card, CharacterViz target)
    {
        
        if (type == null) return;
        if (type.IsSubclassOf(typeof(Buff)) || type == typeof(Buff))
        {
            int count = 0;

            for (int i = 0; i < target.buffList.Count; i++)
            {
                if (target.buffList[i].GetType().IsSubclassOf(type))
                {
                    count+= target.buffList[i].countNum;
                }
            }
            for (int i = 0; i < count; i++)
            {
                keyWord.Activate(caster,card, target);
                if (isDisposable) return;
            }
        }
    }
    public override List<string> GetVariables(CharacterViz caster, CharacterViz target = null)
    {
        return keyWord.GetVariables(caster, target);
    }

}
