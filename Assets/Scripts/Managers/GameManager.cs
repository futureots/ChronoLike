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
        EndEnemyTurn();
    }

    public void StartPlayerTurn()//�Ͻ��۽� �ڽ�Ʈ �ʱ�ȭ, ī���ο�
    {
        // ĳ���� ���� ī��Ʈ => ���� ī��Ʈ�� ��������Ʈ�� �ְ�, ��������Ʈ �Լ� ����
        foreach (var item in characterManager.playableCharacters)
        {
            item.TurnStart();
        }
        

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
        foreach (var item in characterManager.playableCharacters)
        {
            item.TurnEnd();
        }
        //�ڵ� ����
        deckManager.ClearHand();

        characterManager.UpdateCharacter();

        StartEnemyTurn();
        
        
    }
    public void StartEnemyTurn()
    {
        // ĳ���� ���� ī��Ʈ => ���� ī��Ʈ�� ��������Ʈ�� �ְ�, ��������Ʈ �Լ� ����
        foreach (var item in characterManager.aiCharacters)
        {
            item.TurnStart();
        }

        characterManager.UpdateCharacter();

        EndEnemyTurn();
    }
    public void EndEnemyTurn()
    {
        foreach (var item in characterManager.aiCharacters)
        {
            item.TurnEnd();
        }
        characterManager.UpdateCharacter();
        StartPlayerTurn();
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

        //�����ڶ� Ÿ��(������null) �Ҵ�
        bool actable = true;
        if (!cardViz.caster.isActable) actable = false;
        draggable.SetTransform();
        cardViz.caster.CharAction.Invoke();
        int discardNum=2;
        if (actable)
        {
            discardNum = cardViz.Execute(charViz);                                                                      //ī�� �ߵ� �� ������
        }
        deckManager.DiscardCard(draggable.previousSiblingNum,discardNum);


        UpdateGame();
        
                                                        //�����ڶ� Ÿ�� �ʱ�ȭ
        
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
