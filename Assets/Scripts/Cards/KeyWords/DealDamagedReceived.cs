using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamagedReceived : KeyWord
{

    public override void Activate(CharacterViz caster, CharacterViz target)
    {
        if (target == null) return;
        Status hp = Status.GetStatus(caster.statusList, "Hp");
        Status currentHp = Status.GetStatus(caster.statusList, "CurrentHp");
        int damage = hp.value - currentHp.value;
        target.Damaged(damage);
    }

    public override List<string> GetVariables(CharacterViz caster, CharacterViz target = null)
    {
        List<string> variables = new List<string>();
        if (caster == null)
        {
            variables.Add("0");
        }
        else
        {
            Status hp = Status.GetStatus(caster.statusList, "Hp");
            Status currentHp = Status.GetStatus(caster.statusList, "CurrentHp");
            int damage = hp.value - currentHp.value;
            variables.Add(damage.ToString());
        }
        return variables;
    }
}
