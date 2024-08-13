using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CharacterManager : MonoBehaviour
{
    public GameObject characterVizPrefab;


    public Transform playableTransform;
    public List<Character> playableCharacters { get; private set; }
    public Transform aiTransform;
    public  List<Character> aiCharacters { get; private set; } 

    public Character currentTarget;
    private void Start()
    {
        playableCharacters = new();
        aiCharacters = new();
        
    }
    
    public void SetCharacter(List<Character> inData, bool isAi)//캐릭터의 팀확인해서 아군, 적 세팅 
    {
        Transform team;
        List<Character> charList;
        if (isAi)                                                                                  //아군
        {
            charList = aiCharacters;
            team = aiTransform;
        }
        else
        {
            charList = playableCharacters;                                                    //적
            team = playableTransform;
        }

        foreach (var item in inData)
        {                                                                           
            charList.Add(item);
            GameObject actor = Instantiate(characterVizPrefab, team);                        //캐릭터 오브젝트 생성
            actor.name = item.name;
            CharacterViz viz = actor.GetComponent<CharacterViz>();
            viz.LoadCharacter(item);
            if (isAi)
            {
                viz.art.gameObject.transform.localScale = new Vector3(-1, 1, 1);

            }

            ApplyCharAbility(item);

        }

    }

    public void UpdateCharacter()
    {
        List<CharacterViz> viz = new(GetComponentsInChildren<CharacterViz>());
        if (viz == null) return;
        for (int i = viz.Count-1; i >=0; i--)
        {
            viz[i].UpdateCharacter();
            if (viz[i].character.StatIsZero("CurrentHp"))
            {
                DestroyCharacter(viz[i]);
            }
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
        Character inCharacter = inCharacterViz.character;
        GameObject ch = inCharacterViz.gameObject;
        DeplyCharAbility(inCharacter);
        for (int i = 0; i < playableCharacters.Count; i++)
        {
            if (playableCharacters[i].Equals(inCharacter))
            {
                GameManager.currentManager.deckManager.ExhaustCharCard(inCharacter);                   //아군일경우
                playableCharacters.RemoveAt(i);
                Destroy(ch);
                return;
            }
        }
        for (int i = 0; i < aiCharacters.Count; i++)
        {
            if (aiCharacters[i].Equals(inCharacter))
            {
                GameManager.currentManager.aiManager.DestroyAI(inCharacter);                               //적일경우
                aiCharacters.RemoveAt(i);
                Destroy(ch);
                return;
            }
        }
    }


    public void ApplyCharAbility(Character inCharacter)
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

    }
}
