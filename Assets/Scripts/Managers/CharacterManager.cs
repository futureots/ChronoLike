using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CharacterManager : MonoBehaviour
{
    public GameObject characterVizPrefab;


    public Transform playableTransform;
    public List<CharacterViz> playableCharacterList { get; private set; }
    public Transform aiTransform;
    public  List<CharacterViz> aiCharacterList { get; private set; }
    public List<CharacterAI> aiList;

    private void Start()
    {
        playableCharacterList = new();
        aiCharacterList = new();
        aiList = new();
    }
    
    public void SetCharacter(List<CharacterViz> inData, bool isAlly)//캐릭터의 팀확인해서 아군, 적 세팅 
    {
        Transform team;
        List<CharacterViz> charList;
        if (isAlly)                                                                                  //아군
        {
            charList = playableCharacterList;
            team = playableTransform;
        }
        else                                                    //적
        {
            charList = aiCharacterList;
            team = aiTransform;
            
        }

        foreach (var datum in inData)
        {
            
            charList.Add(datum);
            datum.transform.SetParent(team);
            datum.transform.localScale = Vector3.one;
            if (!isAlly)
            {
                datum.art.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            foreach (var item in datum.characterAbility)
            {
                item.Initiate();
            }
            //ApplyCharAbility(item);
            datum.UpdateCharacter();
        }

    }

    public void UpdateCharacter()
    {
        List<CharacterViz> viz = new();
        viz.AddRange(playableCharacterList);
        viz.AddRange(aiCharacterList);
        for (int i = viz.Count-1; i >=0; i--)
        {
            if (viz[i] == null)
            {
                Debug.Log("null");
                continue;
            }
            viz[i].UpdateCharacter();
        }
        if(playableCharacterList.Count == 0) 
        {
            GameManager.currentManager.GameEnd(false);
        }
        else if(aiCharacterList.Count == 0)
        {
            GameManager.currentManager.GameEnd(true);
        }
    }
    public void DestroyCharactersShield(bool IsAlly)
    {
        List<CharacterViz> charList;
        if (IsAlly)
        {
            charList = playableCharacterList;
        }
        else
        {
            charList = aiCharacterList;
        }
        foreach (var item in charList)
        {
            item.DestroyShield();
        }
    }
    public void ExceptCharacter(CharacterViz inCharacterViz)
    {
        foreach (var item in inCharacterViz.characterAbility)
        {
            item.Delete();
        }
        inCharacterViz.transform.SetParent(null);
        if (inCharacterViz.isAlly)                                                 //아군
        {
            Debug.Log(inCharacterViz + "Except");
            playableCharacterList.Remove(inCharacterViz);
            GameManager.currentManager.deckManager.ExhaustCharCard(inCharacterViz);
        }
        else                                                                           //적
        {
            Debug.Log("Destroy :"  + inCharacterViz);
            CharacterAI ai = inCharacterViz.GetComponentInChildren<CharacterAI>();
            aiCharacterList.Remove(inCharacterViz);
            aiList.Remove(ai);
        }
        
        Destroy(inCharacterViz.gameObject);
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
