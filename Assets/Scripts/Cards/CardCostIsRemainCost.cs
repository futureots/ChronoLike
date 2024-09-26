using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCostIsRemainCost : CardCost
{

    protected override void Update()
    {
        value = GameManager.currentManager.costManager.costCylinder.colorCost[colorType];
        base.Update();
    }
}
