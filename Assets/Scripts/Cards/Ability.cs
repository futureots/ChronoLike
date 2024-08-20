using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ability
{
    public KeyWord effect;
    public TargetType type;
    public Ability(KeyWord inKeyWord, TargetType targetType=null)
    {
        effect = inKeyWord;
        type = targetType;
    }
    public void Execute(CardViz cardViz)
    {
        if (effect == null) return;
        CharacterViz caster = cardViz.caster;
        List<CharacterViz> targets = new List<CharacterViz>();
        if (type != null)
        {
            targets.AddRange(type.GetTarget(caster.isAlly));
        }
        foreach (CharacterViz target in targets) 
        {
            effect.Activate(caster, cardViz, target);

        }
    }
}
//프로퍼티, 키워드 타겟타입은 3개 커플링
