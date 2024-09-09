using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager gameResources;
    public GameObject CardPrefab;
    public GameObject CharPrefab;
    public GameObject AIPrefab;


    [System.Serializable]
    public struct stringResource
    {
        public string character;
        public List<string> cards;
    }
    [System.Serializable]
    public struct stringEnemyResource
    {
        public string character;
        public CharacterAI ai;
    }
    public List<stringResource> allies;
    public List<stringEnemyResource> enemies;

    public struct AllyData
    {
        public CharacterData characterData;
        public List<CardData> cardDatas;
    }
    List<AllyData> alliesData;




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

        Dictionary<ColorType, int> costList = CreateCost();                                                                              //코스트 세팅

        if (gameManager.costManager != null)                                                               //코스트매니저 코스트 세팅
        {
            gameManager.costManager.SetCostList(costList);
        }




        List<CharacterViz> allyList = new();                                                                                 //아군 캐릭터 생성
        List<CardViz> cards = new();                                                                                      //덱 생성
        foreach (var item in allies)
        {
            string path = Path.Combine(Application.dataPath + "/Data/Characters", item.character + ".json");
            string data = null;
            if (File.Exists(path))
            {
                data = File.ReadAllText(path);
            }
            CharacterData characterData = CharacterData.DeserializeCardData(data);
            CharacterViz charObj = Instantiate(CharPrefab, this.transform).GetComponent<CharacterViz>();
            charObj.LoadCharacter(characterData, true);
            allyList.Add(charObj);

            foreach (var card in item.cards)
            {
                string cardPath = Path.Combine(Application.dataPath + "/Data/Cards", card + ".json");
                string cardStringData = null;
                if (File.Exists(cardPath))
                {
                    cardStringData = File.ReadAllText(cardPath);
                }
                CardData cardData = CardData.DeserializeCardData(cardStringData);
                CardViz cardObj = Instantiate(CardPrefab, this.transform).GetComponent<CardViz>();
                cardObj.LoadCard(cardData, charObj);
                cards.Add(cardObj);
            }
        }
        gameManager.characterManager.SetCharacter(allyList, true);
        gameManager.deckManager.SetDeck(cards);
            





        List<CharacterViz> enemyList = new();
        foreach (var item in enemies)
        {                                                                                                            //적 캐릭터 생성
            string path = Path.Combine(Application.dataPath + "/Data/Characters", item.character + ".json");
            string data = null;
            if (File.Exists(path))
            {
                data = File.ReadAllText(path);
            }
            CharacterData characterData = CharacterData.DeserializeCardData(data);
            CharacterViz charObj = Instantiate(CharPrefab, this.transform).GetComponent<CharacterViz>();
            charObj.LoadCharacter(characterData, false);
            enemyList.Add(charObj);

            GameObject aiObj = Instantiate(AIPrefab, transform);
            CharacterAI ai = aiObj.AddComponent<SlimeAI>();
            aiObj.name = ai.GetType().Name;
            ai.SetAI(charObj);
            

            gameManager.characterManager.aiList.Add(ai);
        }
        gameManager.characterManager.SetCharacter(enemyList, false);



        /*if (gameManager.aiManager != null)
        {
            gameManager.aiManager.SetAI(gameManager.characterManager.aiTransform);
        }*/






        



    }

    public Dictionary<ColorType, int> CreateCost()
    {
        //캐릭터 리소스에서 캐릭터 컬러타입을 추출
        Dictionary<ColorType, int> costList = new();
        
        //추출한 컬러타입으로 코스트엘리먼트 생성
        for(int i = 0; i < 5; i++)
        {
            ColorType colorType = (ColorType)i;
            int cost = 2;
            costList.Add(colorType, cost);
        }
        return costList;
    }


}
