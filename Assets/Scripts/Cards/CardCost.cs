using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardCost
{
    public CardViz card;
    public ColorType colorType;
    public int basicValue;
    public int currentValue;

    public CardCost() : this(1) { }
    public CardCost(int value)
    {
        basicValue = value;
        currentValue = value;
    }
    public virtual void UpdateCostValue()
    {
        
    }

}

