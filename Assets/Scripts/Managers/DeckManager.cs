using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{

    public List<Transform> deckTransforms;                             //0 핸드,1 : 덱, 2 : 버린덱 , 3: 소멸덱 만약 추가할거 있으면 배열 길이 늘리기
    public List<Card>[] decks;                                               //0 핸드,1 : 덱, 2 : 버린덱 , 3: 소멸덱 만약 추가할거 있으면 배열 길이 늘리기
    public GameObject cardPrefab;

    public int turnDrawCount;
    private void Awake()
    {
        decks = new List<Card>[4];
        for (int i = 0; i < 4; i++)
        {
            decks[i] = new List<Card>();
        }

    }

    public void SetDeck(List<Card> deckData)
    {
        decks[1].AddRange(deckData);
        for(int i = 0; i < decks[1].Count; i++)
        {
            GameObject card = Instantiate(cardPrefab, deckTransforms[1]);
            Clickable click = SetClickable(card);
            click.cardClicked = new( ()=> { Debug.Log("Added"); });
            CardViz cardViz = card.GetComponent<CardViz>();
            cardViz.LoadCard(decks[1][i]);
        }
        
        //복사가 필요하면 밑에거 사용
        /*foreach (var item in deckData)                                              
        {
            Card card = new(item.data,item.caster);
            deck.Add(card);
        }*/
    }
    public void TurnStartDraw()
    {
        for (int i = 0; i < turnDrawCount; i++)
        {
            DrawCard();
        }
    }
    public GameObject GetRandomCard(int deckNum)
    {
        GameObject card = null;
        if(decks[deckNum].Count > 0)
        {
            int num = Random.Range(0, decks[deckNum].Count);
            card = PopCard(deckNum, num);
        }

        return card;

    }
    public void DrawCard()
    {
        
        if (decks[1].Count == 0)
        {
            Debug.Log("No More Cards");
            return;
        }
        GameObject card = GetRandomCard(1);
        SetDraggable(card);
        AddCard(0, card);
        if (decks[1].Count == 0) RecycleDeck();
    }
    public void RecycleDeck()
    {
        for (int i = decks[2].Count - 1; i >= 0; i--) 
        {
            GameObject obj = PopCard(2, i);
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

    public void ExhaustCharCard(Character inDeadChar)                      //캐릭터 죽으면 캐릭터 카드 전부 소멸시키기
    {

        for (int i = 0; i < 3; i++)
        {
            Debug.Log("deck" + i + " count =" + decks[i].Count + " transform count =" + deckTransforms[i].childCount);
            for (int j = 0; j < decks[i].Count; j++)
            {
                if (decks[i][j].caster.Equals(inDeadChar))
                {
                    GameObject obj = PopCard(i, j);
                    AddCard(3, obj);
                    j--;
                }
            }

        }
    }
    public void DiscardCard(int inCardNum,int inDeckNum)
    {
        GameObject obj = PopCard(0, inCardNum);
        SetClickable(obj);
        
        AddCard(inDeckNum,obj);
    }
    public GameObject PopCard(int deckNum, int cardNum)
    {
        if (decks[deckNum].Count <= cardNum) return null;
        //Debug.Log(deckTransforms[deckNum].childCount + "and "+ decks[deckNum].Count+ "is" + cardNum);
        GameObject cardObj = deckTransforms[deckNum].GetChild(cardNum).gameObject;
        decks[deckNum].RemoveAt(cardNum);
        return cardObj;
    }
    public void AddCard(int deckNum, GameObject cardObj)
    {
        CardViz cardViz = cardObj.GetComponent<CardViz>();
        if (cardViz == null) return;
        Card card = cardViz.card;
        decks[deckNum].Add(card);
        cardObj.transform.SetParent(deckTransforms[deckNum]);
    }

    public Draggable SetDraggable(GameObject obj)
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
    public Clickable SetClickable(GameObject obj)
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
}
