using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCostIsRemainCost : CardCost
{
    public CardCostIsRemainCost()
    {
        basicValue = -1;
        currentValue = -1;
    }
    public override void UpdateCostValue()
    {
        currentValue = GameManager.currentManager.costManager.costCylinder.colorCost[colorType];
    }
}
