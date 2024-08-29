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
    public override void Init(CharacterViz inCharacter)
    {
        if (inCharacter == null) return;
        target = inCharacter;

        target.TurnStart += new CharacterViz.AbilityActivate(BuffCountDown);
        
        //Debug.Log("Buff Attached/ " + target.name + " // Dot count = " + countNum);
    }

    public override void DetachBuff()
    {
        Debug.Log("DetachBuff");
        target.TurnStart -= new CharacterViz.AbilityActivate(BuffCountDown);
        target = null;
    }
    public void BuffCountDown()
    {
        target.Damaged(countNum);
        countNum = Mathf.Max(countNum - 1, 0);
    }

    public override bool MergeBuff(Buff inBuff)
    {
        Poison inDot = inBuff as Poison; 
        if(inDot == null) return false;
        countNum += inDot.countNum;
        //Debug.Log("BuffCount = " + countNum);
        return true;
    }

    public override Buff Clone()
    {
        Poison copy = new Poison(countNum);
        return copy;
    }
}
