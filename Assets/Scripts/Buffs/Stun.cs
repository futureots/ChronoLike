using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : Buff
{
    public Stun()
    {
        iconPath = "Assets/Data/Sprites\\Stun.png";
        countNum = 0;
        target = null;
    }
    public Stun(int inNum)
    {
        iconPath = "Assets/Data/Sprites\\Stun.png";
        countNum = inNum;
        target = null;
    }
    public override Buff Clone()
    {
        Stun copy = new Stun(countNum);
        return copy;
    }

    public override void Init(CharacterViz inCharacter)
    {
        if (inCharacter == null) return;
        target = inCharacter;
        target.isActable = false;
        target.CharAction += new CharacterViz.AbilityActivate(BuffCountDown);
    }
    public void BuffCountDown()
    {
        countNum = Mathf.Max(countNum-1, 0);
        Debug.Log("StunCount =" + countNum);
    }
    public override void DetachBuff()
    {
        target.isActable = true;
        target.CharAction -= new CharacterViz.AbilityActivate(BuffCountDown);
        target = null;
    }
    public override bool MergeBuff(Buff inBuff)
    {
        Stun inDot = inBuff as Stun;
        if (inDot == null) return false;
        countNum += inDot.countNum;
        return true;
    }
}
