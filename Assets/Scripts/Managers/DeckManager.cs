using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{

    public List<Transform> deckTransforms;                             //0 핸드,1 : 덱, 2 : 버린덱 , 3: 소멸덱 만약 추가할거 있으면 배열 길이 늘리기
    public List<CardViz>[] decks;                                               //0 핸드,1 : 덱, 2 : 버린덱 , 3: 소멸덱 만약 추가할거 있으면 배열 길이 늘리기
    public GameObject cardPrefab;

    public int turnDrawCount;
    private void Awake()
    {
        decks = new List<CardViz>[4];
        for (int i = 0; i < 4; i++)
        {
            decks[i] = new List<CardViz>();
        }

    }

    public void SetDeck(List<CardViz> deckData)
    {
        decks[1].AddRange(deckData);
        for(int i = 0; i < decks[1].Count; i++)
        {
            deckData[i].transform.SetParent(deckTransforms[1]);
            deckData[i].transform.localScale = Vector3.one;
            Clickable click = SetClickable(deckData[i].gameObject);
            click.cardClicked = new(() => { Debug.Log("Added"); });
        }
    }
    public void TurnStartDraw()
    {
        for (int i = 0; i < turnDrawCount; i++)
        {
            CardViz card = GetRandomCard(1);
            DrawCard(card);
        }
    }
    public CardViz GetRandomCard(int deckNum)
    {
        CardViz card = null;
        if(decks[deckNum].Count > 0)
        {
            int num = Random.Range(0, decks[deckNum].Count);
            card = PopCard(deckNum, num);
        }

        return card;

    }
    public void DrawCard(CardViz card)
    {
        if (decks[0].Contains(card))
        {
            Debug.Log("Already Drawn");
            return;
        }
        
        SetDraggable(card.gameObject);
        AddCard(0, card);
        CardViz viz = card.GetComponent<CardViz>();
        viz.AbilityInit();
        if (decks[1].Count == 0) RecycleDeck();
    }
    public void RecycleDeck()
    {
        Debug.Log("RecycleDeck");
        for (int i = decks[2].Count - 1; i >= 0; i--) 
        {
            CardViz obj = PopCard(2, i);
            AddCard(1, obj);
        }
    }
    public void ClearHand()
    {
        for (int i = deckTransforms[0].childCount - 1; i >= 0; i--)
        {
            DiscardCard(i, 2);
        }
    }

    public void ExhaustCharCard(CharacterViz inDeadChar)                      //캐릭터 죽으면 캐릭터 카드 전부 소멸시키기
    {
        
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("deck" + i + " count =" + decks[i].Count + " transform count =" + deckTransforms[i].childCount);
            for (int j = 0; j < decks[i].Count; j++)
            {
                if (decks[i][j].caster.Equals(inDeadChar))
                {
                    CardViz obj = PopCard(i, j);
                    AddCard(3, obj);
                    j--;
                }
            }

        }
    }
    public void DiscardCard(int inCardNum,int inDeckNum)
    {
        CardViz obj = PopCard(0, inCardNum);
        SetClickable(obj.gameObject);
        AddCard(inDeckNum,obj);
    }
    public void UpdateDeck()
    {
        for (int i = 0; i < 3; i++)
        {
            foreach (var item in decks[i])
            {
                item.UpdateAbilityDescribtion();
            }
        }
    }

    CardViz PopCard(int deckNum, int cardNum)
    {
        if (decks[deckNum].Count <= cardNum) return null;
        //Debug.Log(deckTransforms[deckNum].childCount + "and "+ decks[deckNum].Count+ "is" + cardNum);
        CardViz cardObj = decks[deckNum][cardNum];
        decks[deckNum].Remove(cardObj);
        return cardObj;
    }
    public void AddCard(int deckNum, CardViz cardViz)
    {
        if (cardViz == null) return;
        decks[deckNum].Add(cardViz);
        cardViz.transform.SetParent(deckTransforms[deckNum]);
    }

    Draggable SetDraggable(GameObject obj)
    {
        if(obj.GetComponent<Clickable>() != null)
        {
            obj.GetComponent<Clickable>().enabled = false;
        }
        Draggable draggable;
        draggable = obj.GetComponent<Draggable>();
        if (draggable == null)
        {
            draggable = obj.AddComponent<Draggable>();
        }
        draggable.enabled = true;
        draggable.RaycastTarget = obj.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>();
        return draggable;
    }
    Clickable SetClickable(GameObject obj)
    {
        if (obj.GetComponent<Draggable>() != null)
        {
            obj.GetComponent<Draggable>().enabled = false;
        }
        Clickable clickable;
        clickable = obj.GetComponent <Clickable>();
        if (clickable == null)
        {
            clickable = obj.AddComponent<Clickable>();
        }
        clickable.enabled = true;
        return clickable;
    }

    public List<GameObject> SearchCards(int deckIndex)
    {
        List<GameObject> cards = new List<GameObject>();
        foreach (var item in decks[deckIndex])
        {
            cards.Add(item.gameObject);
        }
        return cards;
    }
    public List<GameObject> SearchCards(string casterName)
    {
        List<GameObject> cards = new List<GameObject>();
        for(int i = 0; i < 3; i++)
        {
            foreach (var item in decks[i])
            {
                if (item.caster.characterName.Equals(casterName))
                {
                    cards.Add(item.gameObject);
                }
            }
        }
        return cards;
    }
}
