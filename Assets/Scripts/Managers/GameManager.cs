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

    public void StartPlayerTurn()//�Ͻ��۽� �ڽ�Ʈ �ʱ�ȭ, ī���ο�
    {
        // ĳ���� ���� ī��Ʈ => ���� ī��Ʈ�� ��������Ʈ�� �ְ�, ��������Ʈ �Լ� ����
        PlayerTurnStart();
        characterManager.UpdateCharacter();


        //�ڽ�Ʈ �ʱ�ȭ
        costManager.FillCost();

        //������ ��ο�
        deckManager.TurnStartDraw();


        characterManager.UpdateCharacter();
    }
    public void EndPlayerTurn()//������� �Ǹ����� ���� ���� ����, �ڵ� ����
    {
        //�����ڽ�Ʈ ����
        costManager.costCylinder.ClearCylinder();

        //Ư�� Ű���� ���
        PlayerTurnEnd();
        //�ڵ� ����
        deckManager.ClearHand();


        characterManager.UpdateCharacter();

        StartEnemyTurn();
        
        
    }
    public void StartEnemyTurn()
    {
        // ĳ���� ���� ī��Ʈ => ���� ī��Ʈ�� ��������Ʈ�� �ְ�, ��������Ʈ �Լ� ����
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

        //�����ڶ� Ÿ��(������null) �Ҵ�
        
        characterManager.currentTarget = target;

        draggable.SetTransform();
        
        int discardNum = card.Execute(this);                                                                      //ī�� �ߵ� �� ������

        deckManager.DiscardCard(draggable.previousSiblingNum,discardNum);

        
        characterManager.UpdateCharacter();                                                      //ĳ���� ������Ʈ
        CardViz[] handCard = deckManager.deckTransforms[0].GetComponentsInChildren<CardViz>();
        foreach (var item in handCard)
        {
            item.UpdateAbility();
        }
        
                                                        //�����ڶ� Ÿ�� �ʱ�ȭ
        characterManager.currentTarget = null;
        
    }






    public void GameEnd(bool isPlayerWin)
    {
        gameEndObj.SetActive(true);
        gameEndObj.transform.GetChild(0).gameObject.SetActive(isPlayerWin);
        gameEndObj.transform.GetChild(1).gameObject.SetActive(!isPlayerWin);    
    }


}
