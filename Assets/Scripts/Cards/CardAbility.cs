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
    public void Execute(CardViz cardViz, CharacterViz target)                 //target = ������ null �Է�
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
            effect.Activate(caster, charViz);

        }
    }
}
//������Ƽ, Ű���� Ÿ��Ÿ���� 3�� Ŀ�ø�
