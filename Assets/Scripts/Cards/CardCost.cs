using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCost : MonoBehaviour
{
    public CardViz card;
    public ColorType colorType;
    public int value;
    
}
public class CardCostIsRemainCost : CardCost
{

    private void Update()
    {
        value = GameManager.currentManager.costManager.costCylinder.colorCost[colorType];
    }
}