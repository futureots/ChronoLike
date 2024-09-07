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

    public IEnumerator StartPlayerTurn()//�Ͻ��۽� �ڽ�Ʈ �ʱ�ȭ, ī���ο�
    {
        // ĳ���� ���� ī��Ʈ => ���� ī��Ʈ�� ��������Ʈ�� �ְ�, ��������Ʈ �Լ� ����
        List<CharacterViz> charList = characterManager.playableCharacterList;
        for(int i=charList.Count-1;i>=0;i--)
        {
            charList[i].TurnStart.Invoke();
        }

        //�ڽ�Ʈ �ʱ�ȭ
        costManager.FillCost();

        //������ ��ο�
        deckManager.TurnStartDraw();


        characterManager.UpdateCharacter();
                yield return null;
    }
    public void EndPlayerTurnBtn()
    {
        StartCoroutine(EndPlayerTurn());
    }
    public IEnumerator EndPlayerTurn()//������� �Ǹ����� ���� ���� ����, �ڵ� ����
    {
        //�����ڽ�Ʈ ����
        costManager.costCylinder.ClearCylinder();

        //Ư�� Ű���� ���
        List<CharacterViz> charList = characterManager.playableCharacterList;
        for (int i = charList.Count - 1; i >= 0; i--)
        {
            charList[i].TurnEnd.Invoke();
        }
        yield return null;
        //�ڵ� ����
        deckManager.ClearHand();

        characterManager.DestroyCharactersShield(false);
        characterManager.UpdateCharacter();

        

        StartCoroutine(StartEnemyTurn());
    }
    public IEnumerator StartEnemyTurn()
    {
        // ĳ���� ���� ī��Ʈ => ���� ī��Ʈ�� ��������Ʈ�� �ְ�, ��������Ʈ �Լ� ����
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

        //�����ڶ� Ÿ��(������null) �Ҵ�
        bool actable = cardViz.caster.isActable;

        draggable.SetTransform();
        cardViz.caster.ActBefore.Invoke();
        int discardNum=2;
        if (actable)
        {
            discardNum = cardViz.Execute(charViz);                                                                      //ī�� �ߵ� �� ������
            cardViz.caster.ActAfter.Invoke();
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

    public void SelectUIOpen(List<GameObject> objects, SelectCardUI.SelectedObjectsCommands Command = null)
    {
        selectCardUI.gameObject.SetActive(true);
        selectCardUI.SetObjects(objects, 1, Command);
    }
}
