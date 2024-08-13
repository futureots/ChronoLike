using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ability
{
    GameManager manager;
    Character caster;
    public KeyWord effect;
    public TargetType type;
    public Ability(KeyWord inKeyWord, TargetType targetType=null)
    {
        effect = inKeyWord;
        type = targetType;
        manager = null;
        caster = null;
    }
    public void Execute()
    {
        if (effect == null) return;
        if (manager == null || caster == null) return;
        if (type != null)
        {
            effect.SetTarget(type.GetTarget(manager.characterManager, caster.isAlly));
        }
        else
        {
            effect.SetTarget(caster);
        }
        effect.Activate(manager,caster);
    }
    public void SetAbility(GameManager inManager,Character inCaster)
    {
        manager = inManager;
        caster = inCaster;
    }
}
//프로퍼티, 키워드 타겟타입은 3개 커플링
