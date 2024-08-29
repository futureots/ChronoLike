using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : CharacterAI
{

    public override void SetAction()
    {
        TRandom tRandom = new TRandom(1);
        targets = tRandom.GetTarget(character.isAlly);
        if (targets.Count == 0)
        {
            action = null;
            RenderPanel(null, "");
            return;
        }
        action = new Action(Attack);
        Sprite icon = SpriteConverter.LoadSpriteFile(iconPath + "GreatSword.png");
        Status atk = Status.GetStatus(character.statusList, "Atk");
        //Debug.Log(targets.Count);
        RenderPanel(icon, atk.value.ToString(), targets);
    }
    public void Heal(CharacterViz target)
    {
        //Debug.Log("Enemy Heal");
        Status def = Status.GetStatus(character.statusList, "Def");
        target.EditCharacter("CurrentHp",def.value,Status.Operation.Add);
    }
}
