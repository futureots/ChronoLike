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
    public AIManager aiManager;

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

        aiManager.EnemyPlay();

        characterManager.UpdateCharacter();
        EndEnemyTurn();
    }
    public void EndEnemyTurn()
    {
        EnemyTurnEnd();

        aiManager.SetEnemyHands();
        
        characterManager.UpdateCharacter();
        StartPlayerTurn();
    }





    public void UsePlayerCard(GameObject dragObj, GameObject dropObj)
    {
        CardViz cardViz = dragObj.GetComponent<CardViz>();
        Draggable draggable = dragObj.GetComponent<Draggable>();
        CharacterViz charViz = dropObj.GetComponent<CharacterViz>();
        Character target = null;
        if (cardViz == null) return;
        if(cardViz.card.isNeedTarget)
        {
            if (charViz == null) return;
            target = charViz.character;
        }
        Card card = cardViz.card;
        costManager.FillCardCost(card);

        bool isPaid = costManager.ConsumeCost(card);
        if (!isPaid) return;
        //Debug.Log("Cost Paid");

        //시전자랑 타겟(없으면null) 할당
        
        characterManager.currentTarget = target;

        draggable.SetTransform();
        
        int discardNum = card.Execute(this);                                                                      //카드 발동 및 버리기

        deckManager.DiscardCard(draggable.previousSiblingNum,discardNum);

        
        characterManager.UpdateCharacter();                                                      //캐릭터 업데이트
        CardViz[] handCard = deckManager.deckTransforms[0].GetComponentsInChildren<CardViz>();
        foreach (var item in handCard)
        {
            item.UpdateAbility();
        }
        
                                                        //시전자랑 타겟 초기화
        characterManager.currentTarget = null;
        
    }






    public void GameEnd(bool isPlayerWin)
    {
        gameEndObj.SetActive(true);
        gameEndObj.transform.GetChild(0).gameObject.SetActive(isPlayerWin);
        gameEndObj.transform.GetChild(1).gameObject.SetActive(!isPlayerWin);    
    }


}
