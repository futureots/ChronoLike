using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CharacterManager : MonoBehaviour
{
    public GameObject characterVizPrefab;


    public Transform playableTransform;
    public List<CharacterViz> playableCharacters { get; private set; }
    public Transform aiTransform;
    public  List<CharacterViz> aiCharacters { get; private set; } 

    public CharacterViz currentTarget;
    private void Start()
    {
        playableCharacters = new();
        aiCharacters = new();
        
    }
    
    public void SetCharacter(List<CharacterViz> inData, bool isAlly)//캐릭터의 팀확인해서 아군, 적 세팅 
    {
        Transform team;
        List<CharacterViz> charList;
        if (isAlly)                                                                                  //아군
        {
            charList = playableCharacters;
            team = playableTransform;
        }
        else                                                    //적
        {
            charList = aiCharacters;
            team = aiTransform;
            
        }

        foreach (var item in inData)
        {                                                                           
            charList.Add(item);
            item.transform.SetParent(team);
            item.transform.localScale = Vector3.one;
            if (!isAlly)
            {
                item.art.gameObject.transform.localScale = new Vector3(-1, 1, 1);

            }
            //ApplyCharAbility(item);
            item.UpdateCharacter();
        }

    }

    public void UpdateCharacter()
    {
        List<CharacterViz> viz = new(GetComponentsInChildren<CharacterViz>());
        if (viz == null) return;
        for (int i = viz.Count-1; i >=0; i--)
        {
            viz[i].UpdateCharacter();
        }
        if(playableCharacters.Count == 0) 
        {
            GameManager.currentManager.GameEnd(false);
        }
        else if(aiCharacters.Count == 0)
        {
            GameManager.currentManager.GameEnd(true);
        }
    }
    public void DestroyCharacter(CharacterViz inCharacterViz)
    {
        GameObject ch = inCharacterViz.gameObject;
        //DeplyCharAbility(inCharacter);
        for (int i = 0; i < playableCharacters.Count; i++)
        {
            if (playableCharacters[i].Equals(inCharacterViz))
            {
                GameManager.currentManager.deckManager.ExhaustCharCard(inCharacterViz);                   //아군일경우
                playableCharacters.RemoveAt(i);
                Destroy(ch);
                return;
            }
        }
        for (int i = 0; i < aiCharacters.Count; i++)
        {
            if (aiCharacters[i].Equals(inCharacterViz))                                                                          //적일경우
            {
                aiCharacters.RemoveAt(i);
                Destroy(ch);
                return;
            }
        }
    }


    /*   public void ApplyCharAbility(Character inCharacter)
       {
           if (inCharacter.characterAbility == null) return;

           foreach (var item in inCharacter.characterAbility)
           {
               item.Value.SetAbility(GameManager.currentManager, inCharacter);
               if (inCharacter.isAlly)
               {
                   switch (item.Key)
                   {
                       case "TurnStart":
                           GameManager.currentManager.PlayerTurnStart += new GameManager.AbilityActivate(item.Value.Execute);
                           break;
                       case "TurnEnd":
                           GameManager.currentManager.PlayerTurnEnd += new GameManager.AbilityActivate(item.Value.Execute);
                           break;
                       case "GameStart":
                           GameManager.currentManager.GameStart += new GameManager.AbilityActivate(item.Value.Execute);
                           break;  
                   }
               }
               else
               {
                   switch (item.Key)
                   {
                       case "TurnStart":
                           GameManager.currentManager.EnemyTurnStart += new GameManager.AbilityActivate(item.Value.Execute);
                           break;
                       case "TurnEnd":
                           GameManager.currentManager.EnemyTurnEnd += new GameManager.AbilityActivate(item.Value.Execute);
                           break;
                   }
               }
           }


       }
       public void DeplyCharAbility(Character inCharacter)
       {
           if (inCharacter.characterAbility == null) return;
           foreach (var item in inCharacter.characterAbility)
           {
               if (inCharacter.isAlly)
               {
                   switch (item.Key)
                   {
                       case "TurnStart":
                           GameManager.currentManager.PlayerTurnStart -= new GameManager.AbilityActivate(item.Value.Execute);
                           break;
                       case "TurnEnd":
                           GameManager.currentManager.PlayerTurnEnd -= new GameManager.AbilityActivate(item.Value.Execute);
                           break;
                   }
               }
               else
               {
                   switch (item.Key)
                   {
                       case "TurnStart":
                           GameManager.currentManager.EnemyTurnStart -= new GameManager.AbilityActivate(item.Value.Execute);
                           break;
                       case "TurnEnd":
                           GameManager.currentManager.EnemyTurnEnd -= new GameManager.AbilityActivate(item.Value.Execute);
                           break;
                   }
               }
           }
           //Debug.Log("Team player count = " + playableCharacters.Count + "// Team enemy count = " + aiCharacters.Count);

       }*/
}
