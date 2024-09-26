using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCostIsHandCardCount : CardCost
{
    public CardCostIsHandCardCount()
    {
        basicValue = -1;
        currentValue = -1;
    }
    public override void UpdateCostValue()
    {
        currentValue = GameManager.currentManager.deckManager.decks[0].Count;
    }
}
