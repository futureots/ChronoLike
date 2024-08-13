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

        costList = CreateCost();                                                                              //�ڽ�Ʈ ����

        if (gameManager.costManager != null)                                                               //�ڽ�Ʈ�Ŵ��� �ڽ�Ʈ ����
        {
            gameManager.costManager.SetCostList(costList);
        }




         allyList = new();
        foreach (var item in allies)                                                                    //�Ʊ� ĳ���� ����
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

        if (gameManager.characterManager != null)                                                         //�Ʊ� ĳ���� ����
        {
            gameManager.characterManager.SetCharacter(allyList, false);
        }


        playerDeck = new();                                                                           //�� ����
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
        {                                                                                                            //�� ĳ���� ����
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


        for(int i=0;i<enemies.Count;i++)                                                                             //���� ����
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

        if (gameManager.deckManager != null)                                                                 //���Ŵ��� �� ����
        {
            gameManager.deckManager.SetDeck(playerDeck);
        }




        



    }

    public List<CostElement> CreateCost()
    {
        //ĳ���� ���ҽ����� ĳ���� �÷�Ÿ���� ����
        Dictionary<ColorType, int> costList = new();
        
        //������ �÷�Ÿ������ �ڽ�Ʈ������Ʈ ����
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
