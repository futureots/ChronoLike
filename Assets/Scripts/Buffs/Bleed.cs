using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : Dot
{
    public Bleed() : this(0) { }
    public Bleed(int inCountNum)
    {
        iconPath = "Assets/Data/Sprites\\Bleed.png";
        countNum = inCountNum;
        target = null;
    }

    public override void Init(CharacterViz inCharacter)
    {
        if (inCharacter == null) return;
        target = inCharacter;
        
        target.TurnStart += new CharacterViz.AbilityActivate(BuffCountDown);
        Debug.Log(target + "added");
    }

    public override void DetachBuff()
    {
        //Debug.Log("DetachBuff");
        target.TurnStart -= new CharacterViz.AbilityActivate(BuffCountDown);
        //Debug.Log("Buff Detached/ " + target.name);
        target = null;
    }
    public void BuffCountDown()
    {
        target.Damaged(2);
        countNum = Mathf.Max(countNum - 1, 0);
        //Debug.Log("Dot Count = " + countNum);
    }

    public override bool MergeBuff(Buff inBuff)
    {
        Bleed bleed = inBuff as Bleed;
        if (bleed == null) return false;
        countNum += bleed.countNum;
        return true;
    }

    public override Buff Clone()
    {
        Bleed copy = new Bleed(countNum);
        return copy;
    }
}
