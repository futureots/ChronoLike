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
    


    public override void Activate(GameManager inManager, Character inCaster)
    {
        
        if (type == null) return;
        if (type.IsSubclassOf(typeof(Buff)) || type == typeof(Buff))
        {
            int count = 0;
            foreach (var item in targets)
            {
                
                for (int i = 0; i < item.buffs.Count; i++)
                {
                    if (item.buffs[i].GetType().IsSubclassOf(type))
                    {
                        count+= item.buffs[i].countNum;
                    }
                }
                keyWord.SetTarget(item);
 
            }
            for (int i = 0; i < count; i++)
            {
                keyWord.Activate(inManager, inCaster);
                if (isDisposable) return;
            }
        }
    }
    public override List<string> GetVariables(Character caster, Character target = null)
    {
        return keyWord.GetVariables(caster, target);
    }

}
