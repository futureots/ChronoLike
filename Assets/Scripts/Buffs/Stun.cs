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

    public override void AttachBuff(CharacterViz inCharacter)
    {
        if (inCharacter == null) return;
        target = inCharacter;
        target.buffs.Add(this);
    }
    public void BuffCountDown()
    {
        countNum = Mathf.Max(countNum-1, 0);
        if (countNum == 0) DetachBuff();
    }
    public override void DetachBuff()
    {
        if (target.buffs.Contains(this))
        {
            target.buffs.Remove(this);
            target = null;
        }
    }
    

}
