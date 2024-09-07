using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject gameEndObj;



    public static GameManager currentManager;

    public DeckManager deckManager;
    public CostManager costManager;
    public CharacterManager characterManager;
    public SelectCardUI selectCardUI;


    public delegate void AbilityActivate();
    public AbilityActivate GameStart;
    
    
    private void Awake()
    {
        currentManager = this;
        GameStart = new(() => { });
    }
    private void Start()
    {
        ResourceManager.gameResources.GetResources(this);
        GameStart();
        StartCoroutine(EndEnemyTurn());
    }

    public IEnumerator StartPlayerTurn()//턴시작시 코스트 초기화, 카드드로우
    {
        // 캐릭터 버프 카운트 => 버프 카운트를 델리게이트에 넣고, 델리게이트 함수 실행
        List<CharacterViz> charList = characterManager.playableCharacterList;
        for(int i=charList.Count-1;i>=0;i--)
        {
            charList[i].TurnStart.Invoke();
        }

        //코스트 초기화
        costManager.FillCost();

        //덱에서 드로우
        deckManager.TurnStartDraw();


        characterManager.UpdateCharacter();
                yield return null;
    }
    public void EndPlayerTurnBtn()
    {
        StartCoroutine(EndPlayerTurn());
    }
    public IEnumerator EndPlayerTurn()//턴종료시 실린더에 남은 마나 제거, 핸드 제거
    {
        //남은코스트 제거
        costManager.costCylinder.ClearCylinder();

        //특정 키워드 사용
        List<CharacterViz> charList = characterManager.playableCharacterList;
        for (int i = charList.Count - 1; i >= 0; i--)
        {
            charList[i].TurnEnd.Invoke();
        }
        yield return null;
        //핸드 제거
        deckManager.ClearHand();

        characterManager.DestroyCharactersShield(false);
        characterManager.UpdateCharacter();

        

        StartCoroutine(StartEnemyTurn());
    }
    public IEnumerator StartEnemyTurn()
    {
        // 캐릭터 버프 카운트 => 버프 카운트를 델리게이트에 넣고, 델리게이트 함수 실행
        List<CharacterViz> charList = characterManager.aiCharacterList;
        for (int i = charList.Count - 1; i >= 0; i--)
        {
            charList[i].TurnStart.Invoke();
        }
        yield return null;
        foreach (var item in characterManager.aiList)
        {
            item.CharAction();
            yield return new WaitForSeconds(1f);
            characterManager.UpdateCharacter();
        }
        
        yield return null;
        StartCoroutine(EndEnemyTurn());
    }
    public IEnumerator EndEnemyTurn()
    {
        List<CharacterViz> charList = characterManager.aiCharacterList;
        for (int i = charList.Count - 1; i >= 0; i--)
        {
            charList[i].TurnEnd.Invoke();
        }
        yield return null;
        foreach (var item in characterManager.aiList)
        {
            item.SetAction();
        }
        characterManager.DestroyCharactersShield(true);
        characterManager.UpdateCharacter();

        yield return null;
        StartCoroutine(StartPlayerTurn());
    }





    public void UsePlayerCard(GameObject dragObj, GameObject dropObj)
    {
        CardViz cardViz = dragObj.GetComponent<CardViz>();
        Draggable draggable = dragObj.GetComponent<Draggable>();
        CharacterViz charViz = dropObj.GetComponent<CharacterViz>();
        if (cardViz == null) return;
        if(cardViz.isNeedTarget)
        {
            if (charViz == null) return;
        }
        
        costManager.FillCardCost(cardViz);

        bool isPaid = costManager.ConsumeCost(cardViz);
        if (!isPaid) return;
        Debug.Log("Cost Paid");

        //시전자랑 타겟(없으면null) 할당
        bool actable = cardViz.caster.isActable;

        draggable.SetTransform();
        cardViz.caster.ActBefore.Invoke();
        int discardNum=2;
        if (actable)
        {
            discardNum = cardViz.Execute(charViz);                                                                      //카드 발동 및 버리기
            cardViz.caster.ActAfter.Invoke();
        }
        deckManager.DiscardCard(draggable.previousSiblingNum,discardNum);


        UpdateGame();
        
                                                        //시전자랑 타겟 초기화
        
    }
    public void UpdateGame()
    {
        characterManager.UpdateCharacter();
        deckManager.UpdateDeck();
    }





    public void GameEnd(bool isPlayerWin)
    {
        gameEndObj.SetActive(true);
        gameEndObj.transform.GetChild(0).gameObject.SetActive(isPlayerWin);
        gameEndObj.transform.GetChild(1).gameObject.SetActive(!isPlayerWin);    
    }

    public void SelectUIOpen(List<GameObject> objects, SelectCardUI.SelectedObjectsCommands Command = null)
    {
        selectCardUI.gameObject.SetActive(true);
        selectCardUI.SetObjects(objects, 1, Command);
    }
}
