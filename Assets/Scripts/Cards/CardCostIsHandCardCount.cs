using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCostIsHandCardCount : MonoBehaviour
{
    protected override void Update()
    {
        value = GameManager.currentManager.deckManager.decks[0].Count;
        base.Update();
    }
}
