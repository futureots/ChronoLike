using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBuff : Buff
{
    public string statusName;

    public StatusBuff()
    {
        target = null;
        statusName = null;
        countNum = 0;
        iconPath = null;
    }
    public StatusBuff(int inCountNum, string inStatusName)
    {
        target = null;
        statusName = inStatusName;
        countNum = inCountNum;
        switch (statusName)
        {
            case "Atk":
                iconPath = "Assets/Data/Sprites\\Knife.png";
                break;
            case "Def":
                iconPath = "Assets/Data/Sprites\\Shield.png";
                break;
            case "Hp":
                iconPath = "Assets/Data/Sprites\\Heart.png";
                break;
            default:
                iconPath = "Assets/Data/Sprites\\Amumu.png";
                break;
        }
    }
    public override void Init(CharacterViz inCharacter)
    {
        if (inCharacter == null) return;
        target = inCharacter;
        target.EditCharacter(statusName, countNum, Status.Operation.Add);
        Debug.Log("Buff Attached/ " + target.name + " // "+statusName + " = " + Status.GetStatus(target.statusList, statusName).value);
    }

    public override void DetachBuff()
    {
        if (!target.buffList.Contains(this)) return;

        target.buffList.Remove(this);
        target.EditCharacter(statusName, -countNum, Status.Operation.Add);
        Debug.Log("Buff Detached/ " + target.name + " // " + statusName + " = " + Status.GetStatus(target.statusList, statusName).value);
        target = null;
    }

    public override bool MergeBuff(Buff inBuff)
    {
        StatusBuff inStatusBuff = inBuff as StatusBuff;
        if (inStatusBuff == null) return false;
        Debug.Log("inBuff is : " + inStatusBuff.statusName);
        if (!inStatusBuff.statusName.Equals(statusName)) return false;
        
        target.EditCharacter(statusName, inStatusBuff.countNum, Status.Operation.Add);
        countNum += inStatusBuff.countNum;
        Debug.Log("BuffCount = " + countNum + " // " + statusName + " = " + Status.GetStatus(target.statusList, statusName).value);
        if (countNum == 0) DetachBuff();
        return true;
        
    }
    public override Buff Clone()
    {
        StatusBuff copy = new StatusBuff(countNum,statusName);
        
        return copy;
    }
}
