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
    public void Execute(Card card)
    {
        if (effect == null) return;
        Character caster = card.caster;
        List<Character> targets = new List<Character>();
        if (type != null)
        {
            targets.AddRange(type.GetTarget(caster.isAlly));
        }
        foreach (Character target in targets) 
        {
            effect.Activate(caster, card, target);

        }
    }
}
//프로퍼티, 키워드 타겟타입은 3개 커플링
