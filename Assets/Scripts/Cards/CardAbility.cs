using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardAbility
{
    public KeyWord effect;
    public TargetType type;
    public CardAbility(KeyWord inKeyWord, TargetType targetType=null)
    {
        effect = inKeyWord;
        type = targetType;
    }

    public void Delete()
    {
        effect.Delete();
    }
    public void Activate(CardViz cardViz, CharacterViz target)                 //target = 없으면 null 입력
    {
        if (effect == null) return;
        CharacterViz caster = cardViz.caster;
        List<CharacterViz> targets = new List<CharacterViz>();
        if (type == null)
        {
            targets.Add(caster);
        }
        else if(type is TTarget)
        {
            targets.Add(target);
        }
        else
        {
            targets.AddRange(type.GetTarget(caster.isAlly));
        }
        foreach (CharacterViz charViz in targets)
        {
            effect.Active(cardViz,charViz);

        }
    }
}
//프로퍼티, 키워드 타겟타입은 3개 커플링
