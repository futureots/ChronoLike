using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : Dot
{
    public Bleed()
    {
        iconPath = "Assets/Data/Sprites\\Bleed.png";
        countNum = 0;
        target = null;
    }
    public Bleed(int inCountNum)
    {
        iconPath = "Assets/Data/Sprites\\Bleed.png";
        countNum = inCountNum;
        target = null;
    }

    public override void AttachBuff(CharacterViz inCharacter)
    {
        if (inCharacter == null) return;
        target = inCharacter;
        target.buffs.Add(this);
        if (target.isAlly)
        {
            GameManager.currentManager.PlayerTurnStart += new GameManager.AbilityActivate(BuffCountDown);
        }
        else
        {
            GameManager.currentManager.EnemyTurnStart += new GameManager.AbilityActivate(BuffCountDown);
        }
    }

    public override void DetachBuff()
    {
        if (target.isAlly)
        {
            GameManager.currentManager.PlayerTurnStart -= new GameManager.AbilityActivate(BuffCountDown);
        }
        else
        {
            GameManager.currentManager.EnemyTurnStart -= new GameManager.AbilityActivate(BuffCountDown);
        }



        if (!target.buffs.Contains(this)) return;

        target.buffs.Remove(this);
        //Debug.Log("Buff Detached/ " + target.name);
        target = null;
    }
    public void BuffCountDown()
    {
        target.EditCharacter("CurrentHp", -2, Status.Operation.Add);
        countNum = Mathf.Max(countNum - 1, 0);
        //Debug.Log("Dot Count = " + countNum);
        if (countNum == 0) DetachBuff();
    }

    public override bool MergeBuff(Buff inBuff)
    {
        Bleed bleed = inBuff as Bleed;
        if (bleed == null) return false;
        countNum += bleed.countNum;
        //Debug.Log("BuffCount = " + countNum);
        if (countNum == 0) DetachBuff();
        return true;
    }

    public override Buff Clone()
    {
        Bleed copy = new Bleed(countNum);
        return copy;
    }
}
