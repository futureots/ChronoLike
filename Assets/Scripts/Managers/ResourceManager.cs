using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager gameResources;


    [System.Serializable]
    public struct Resource
    {
        public string character;
        public List<string> cards;
    }
    [System.Serializable]
    public struct EnemyResource
    {
        public string character;
        public List<string> cards;
        public int actionCount;
    }
    public List<Resource> allies;
    public List<EnemyResource> enemies;

    List<Card> playerDeck;
    List<Character> allyList,enemyList;
    List<CostElement> costList;


    public enum Reward
    {
        Card,
        Gold,
        Relic
    }
    public List<Reward> rewards;
    private void Awake()
    {
        gameResources = this;
        
    }
    public void GetResources(GameManager gameManager)
    {
        if (gameManager == null)
        {
            Debug.LogError("GM is not Allocated");
            return;
        }

        costList = CreateCost();                                                                              //코스트 세팅

        if (gameManager.costManager != null)                                                               //코스트매니저 코스트 세팅
        {
            gameManager.costManager.SetCostList(costList);
        }




         allyList = new();
        foreach (var item in allies)                                                                    //아군 캐릭터 생성
        {
            string path = Path.Combine(Application.dataPath + "/Data/Characters", item.character + ".json");
            string data = null;
            if (File.Exists(path))
            {
                data = File.ReadAllText(path);
            }
            CharacterData characterData = CharacterData.DeserializeCardData(data);
            Character ally = new(characterData, true);
            allyList.Add(ally);
        }

        if (gameManager.characterManager != null)                                                         //아군 캐릭터 세팅
        {
            gameManager.characterManager.SetCharacter(allyList, false);
        }


        playerDeck = new();                                                                           //덱 생성
        for(int i=0;i<allies.Count;i++)
        {
            foreach (var card in allies[i].cards)
            {
                string path = Path.Combine(Application.dataPath + "/Data/Cards", card + ".json");
                string data = null;
                if (File.Exists(path))
                {
                    data = File.ReadAllText(path);
                }
                CardData cardData = CardData.DeserializeCardData(data);
                Card temp = new(cardData,allyList[i]);
                playerDeck.Add(temp);
            }
        }

        enemyList = new();
        foreach (var item in enemies)
        {                                                                                                            //적 캐릭터 생성
            string path = Path.Combine(Application.dataPath + "/Data/Characters", item.character + ".json");
            string data = null;
            if (File.Exists(path))
            {
                data = File.ReadAllText(path);
            }
            CharacterData characterData = CharacterData.DeserializeCardData(data);
            Character enemy = new(characterData, false);
            enemyList.Add(enemy);
        }

        if (gameManager.characterManager != null)
        {
            gameManager.characterManager.SetCharacter(enemyList, true);
        }


        for(int i=0;i<enemies.Count;i++)                                                                             //적덱 생성
        {
            List<Card> enemyDeck = new();

            foreach (var card in enemies[i].cards)
            {
                string path = Path.Combine(Application.dataPath + "/Data/Cards", card + ".json");
                string data = null;
                if (File.Exists(path))
                {
                    data = File.ReadAllText(path);
                }
                CardData cardData = CardData.DeserializeCardData(data);
                Card temp = new(cardData,enemyList[i]);

                enemyDeck.Add(temp);

            }
            if(gameManager.aiManager != null) gameManager.aiManager.GenerateAI(enemyDeck,enemies[i].actionCount);
        }

        if (gameManager.aiManager != null)
        {
            gameManager.aiManager.SetAI(gameManager.characterManager.aiTransform);
        }

        if (gameManager.deckManager != null)                                                                 //덱매니저 덱 세팅
        {
            gameManager.deckManager.SetDeck(playerDeck);
        }




        



    }

    public List<CostElement> CreateCost()
    {
        //캐릭터 리소스에서 캐릭터 컬러타입을 추출
        Dictionary<ColorType, int> costList = new();
        
        //추출한 컬러타입으로 코스트엘리먼트 생성
        List<CostElement> result = new();
        for(int i = 0; i < 5; i++)
        {
            ColorType colorType = (ColorType)i;
            CostElement costElement;
            if (costList.ContainsKey(colorType))
            {
                costElement = new(colorType, costList[colorType]);
            }
            else
            {
                costElement = new(colorType,2);
            }
            result.Add(costElement);
        }
        return result;
    }
    public void SetReward(Reward reward)
    {

    }
}
