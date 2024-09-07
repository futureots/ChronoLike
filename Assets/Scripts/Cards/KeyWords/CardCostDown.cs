using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCostDown : KeyWord
{
    public int subtractValue;

    public override void Active(CardViz cardViz,CharacterViz target)
    {
        List<GameObject> cardList =GameManager.currentManager.deckManager.SearchCards(0);
        if(cardViz!=null) cardList.Remove(cardViz.gameObject);
        GameManager.currentManager.SelectUIOpen(cardList, CostDown);
    }
    public void CostDown(GameObject card)
    {
        CardViz cardViz = card.GetComponent<CardViz>();
        if (cardViz == null) return;
        cardViz.costs[cardViz.cardData.cardColor] = Mathf.Max(cardViz.costs[cardViz.cardData.cardColor] -subtractValue, 0);
        cardViz.UpdateAbilityDescribtion();
    }

    public override List<string> GetVariables(CharacterViz target = null)
    {
        List<string> variables = new List<string>();
        variables.Add("1");
        return variables;
    }
}
