using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AIManager : MonoBehaviour
{
    public GameObject aiPrefab;
    List<EnemyAI> aiList;
    
    private void Awake()
    {
        aiList = new List<EnemyAI>();
    }
    public void GenerateAI(List<Card> deck, int inActionCount)
    {
        GameObject temp = Instantiate(aiPrefab,transform);
        EnemyAI ai = temp.GetComponent<EnemyAI>();
        if(ai!= null)
        {
            ai.actionCount = inActionCount;
            ai.decks[1].AddRange(deck);
            aiList.Add(ai);

        }
        //Debug.Log("AI Generated Deckcount = " + ai.decks[0].Count);
    }

    public void SetAI(Transform aiTransform)
    {
        for (int i = 0; i < aiTransform.childCount; i++)
        {   
            CharacterViz viz = aiTransform.GetChild(i).GetComponent<CharacterViz>();
            if (viz == null) return;
            if(aiList.Count > i)
            {
                aiList[i].transform.SetParent(viz.transform);
                aiList[i].character = viz.character;


                RectTransform rect = aiList[i].transform as RectTransform;
                rect.localScale = Vector3.one;
                rect.localPosition = new Vector2(0, 250);
                rect.sizeDelta = new Vector2(200,100);
            }
            //Debug.Log("ai Set on :" + viz.character.name);
        }
    }

    public void DestroyAI(Character inCharacter)
    {
        for (int i = 0; i < aiList.Count; i++)
        {
            if (aiList[i].character.Equals(inCharacter))
            {
                aiList.RemoveAt(i);
                //Debug.Log("Ailist count = " + aiList.Count);
                return;
                
            }
        }
    }

    public void SetEnemyHands()
    {
        foreach (var item in aiList)
        {
            item.SetHand();
        }
    }
    public void EnemyPlay()
    {
        foreach (var item in aiList)
        {
            item.PlayHand();
        }
    }
}
