using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff
{
    public string iconPath;
    public Character target;
    public int countNum;

    public abstract Buff Clone();
    public virtual void AttachBuff(Character inCharacter)
    {

    }
    public virtual void DetachBuff()
    {

    }
    public virtual bool MergeBuff(Buff inBuff)
    {
        return false;
    }
}
