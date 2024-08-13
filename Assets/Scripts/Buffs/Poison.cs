using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : Dot
{
    public Poison()
    {
        iconPath = "Assets/Data/Sprites\\Bubble.png";
        countNum = 0;
        target = null;
    }
    public Poison(int inCountNum)
    {
        iconPath = "Assets/Data/Sprites\\Bubble.png";
        countNum = inCountNum;
        target = null;
    }
    public override void AttachBuff(Character inCharacter)
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
            GameManager.currentManager.EnemyTurnStart+= new GameManager.AbilityActivate(BuffCountDown);
        }
        //Debug.Log("Buff Attached/ " + target.name + " // Dot count = " + countNum);
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
        target.EditCharacter("CurrentHp", -countNum, Status.Operation.Add);
        countNum = Mathf.Max(countNum - 1, 0);
        //Debug.Log("Dot Count = " + countNum);
        if (countNum == 0) DetachBuff();
    }

    public override bool MergeBuff(Buff inBuff)
    {
        Poison inDot = inBuff as Poison; 
        if(inDot == null) return false;
        countNum += inDot.countNum;
        //Debug.Log("BuffCount = " + countNum);
        if (countNum == 0) DetachBuff();
        return true;
    }

    public override Buff Clone()
    {
        Poison copy = new Poison(countNum);
        return copy;
    }
}
