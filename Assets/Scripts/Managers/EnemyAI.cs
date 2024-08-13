using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    
    public Character character;
    public Transform hand;
    public int actionCount;

    public GameObject cardPrefab;
    public List<Card>[] decks;                                               //0 : µ¶, 1 : πˆ∏∞µ¶ , 2: º“∏Íµ¶ ∏∏æ‡ √ﬂ∞°«“∞≈ ¿÷¿∏∏È πËø≠ ±Ê¿Ã ¥√∏Æ±‚
    
    private void Awake()
    {
        decks = new List<Card>[4];
        for (int i = 0; i < 4; i++)
        {
            decks[i] = new List<Card>();
        }
    }

    public void EnemyAction(Card card)
    {
        if (card == null) return;
        CharacterManager charManager = GameManager.currentManager.characterManager;
        if (charManager.playableCharacters.Count == 0) return;                                                             //¿”Ω√
        if (card.isNeedTarget)
        {
            TRandom random = new(1, 1);
            charManager.currentTarget = random.GetTarget(charManager, false)[0];
        }


        int discardNum = card.Execute(GameManager.currentManager);
        decks[discardNum].Add(card);
        charManager.UpdateCharacter();
        charManager.currentTarget = null;
    }
    public void SetHand()
    {
        for (int i = 0; i < actionCount; i++)
        {
            Card card = GetRandomCard();
            if(card == null) return;
            GameObject cardObj = Instantiate(cardPrefab,hand.transform);
            CardViz viz = cardObj.GetComponent<CardViz>();
            if (viz == null) return;
            viz.cardTemplate.transform.localScale = new Vector2(0.25f, 0.25f);
            viz.raycastTarget.raycastTarget = false;
            viz.LoadCard(card);
        }
    }
    public void PlayHand()
    {
        for(int i = 0; i < hand.childCount; i++)
        {
            CardViz viz = hand.GetChild(i).GetComponent<CardViz>();
            EnemyAction(viz.card);
        }
        for (int i = hand.childCount - 1; i >= 0; i--) 
        {
            Destroy(hand.GetChild(i).gameObject);
        }

    }

    public Card GetRandomCard()
    {
        if(decks[1].Count == 0)
        {
            RecycleDeck();
        }
        if (decks[1].Count == 0) return null;
        int num = Random.Range(0, decks[1].Count);
        Card draw = decks[1][num];
        decks[1].RemoveAt(num);
        return draw;
    }

    public void RecycleDeck()
    {
        decks[1].AddRange(decks[2]);
        decks[2].Clear();
    }
}
