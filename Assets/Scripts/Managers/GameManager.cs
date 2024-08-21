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

    public delegate void AbilityActivate();
    public AbilityActivate PlayerTurnStart, PlayerTurnEnd,EnemyTurnStart,EnemyTurnEnd,GameStart;
    
    
    private void Awake()
    {
        currentManager = this;
        PlayerTurnStart = new(() => { });
        PlayerTurnEnd = new(() => { });
        EnemyTurnStart = new(() => { });
        EnemyTurnEnd = new(() => { });
        GameStart = new(() => { });
    }
    private void Start()
    {
        ResourceManager.gameResources.GetResources(this);

        GameStart();
        EndEnemyTurn();
    }

    public void StartPlayerTurn()//턴시작시 코스트 초기화, 카드드로우
    {
        // 캐릭터 버프 카운트 => 버프 카운트를 델리게이트에 넣고, 델리게이트 함수 실행
        PlayerTurnStart();
        characterManager.UpdateCharacter();


        //코스트 초기화
        costManager.FillCost();

        //덱에서 드로우
        deckManager.TurnStartDraw();


        characterManager.UpdateCharacter();
    }
    public void EndPlayerTurn()//턴종료시 실린더에 남은 마나 제거, 핸드 제거
    {
        //남은코스트 제거
        costManager.costCylinder.ClearCylinder();

        //특정 키워드 사용
        PlayerTurnEnd();
        //핸드 제거
        deckManager.ClearHand();


        characterManager.UpdateCharacter();

        StartEnemyTurn();
        
        
    }
    public void StartEnemyTurn()
    {
        // 캐릭터 버프 카운트 => 버프 카운트를 델리게이트에 넣고, 델리게이트 함수 실행
        EnemyTurnStart();


        

        characterManager.UpdateCharacter();
        EndEnemyTurn();
    }
    public void EndEnemyTurn()
    {
        EnemyTurnEnd();

        
        characterManager.UpdateCharacter();
        StartPlayerTurn();
    }





    public void UsePlayerCard(GameObject dragObj, GameObject dropObj)
    {
        CardViz cardViz = dragObj.GetComponent<CardViz>();
        Draggable draggable = dragObj.GetComponent<Draggable>();
        CharacterViz charViz = dropObj.GetComponent<CharacterViz>();
        CharacterViz target = null;
        if (cardViz == null) return;
        if(cardViz.isNeedTarget)
        {
            if (charViz == null) return;
            target = charViz;
        }
        
        costManager.FillCardCost(cardViz);

        bool isPaid = costManager.ConsumeCost(cardViz);
        if (!isPaid) return;
        Debug.Log("Cost Paid");

        //시전자랑 타겟(없으면null) 할당
        
        characterManager.currentTarget = target;

        draggable.SetTransform();
        
        int discardNum = cardViz.Execute(this);                                                                      //카드 발동 및 버리기

        deckManager.DiscardCard(draggable.previousSiblingNum,discardNum);


        UpdateGame();
        
                                                        //시전자랑 타겟 초기화
        characterManager.currentTarget = null;
        
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


}
