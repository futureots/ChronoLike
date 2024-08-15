using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card
{
    public CardData data;                                                              //카드 원본
    public Character caster;                                                           //시전자

    public string title;                                                                   //기본 정보값
    public Sprite art;
    public string abilityDescription;
    public string vizDescription { get;  private set; }
    public bool isNeedTarget;


    public ColorType cardColor;                                                       //기본정보값
    public Dictionary<ColorType, int> costs;

    public List<Ability> cardAbility;                                                   //카드 능력(키워드)

    public int discardNum;                                                      //카드 사용후 버려질지 소멸될지, 덱 번호값
    public Card(CardData cardData, Character owner = null)
    {
        LoadCardData(cardData, owner);
    }
    public void LoadCardData(CardData cardData, Character owner = null)
    {
        if (cardData == null) return;
        data = cardData;
        title = cardData.title;
        art = SpriteConverter.LoadSpriteFile(cardData.imagePath);
        abilityDescription = cardData.abilityDescription;
        caster = owner;
        isNeedTarget = cardData.isNeedTarget;
        cardColor = cardData.cardColor;
        costs = cardData.costs;
        cardAbility = cardData.cardAbility;
    }
    public int Execute(GameManager manager)//대상 없음, 대신 캐릭터 매니저가 대상 관리
    {
        if (manager == null)
        {
            Debug.Log("NO MANAGER");
            return-1;
        }

        discardNum = 2;

        if (cardAbility == null) return-1;
        foreach (var ability in cardAbility)
        {
            ability.Execute(this);
        }
        if (discardNum < 0 || discardNum > 3) return-1;
        return discardNum;
    }




    public void UpdateAbilityDescribtion()
    {
        if (cardAbility.Count <= 0) return;
        List<string> abilityVariable = new();
        foreach (var item in cardAbility)
        {
            //Debug.Log(item.effect.GetVariables(caster).Count);
            abilityVariable.AddRange(item.effect.GetVariables(caster));
        }
        string[] values = new string[abilityVariable.Count];
        for(int i = 0; i < values.Length; i++)
        {
            values[i] = abilityVariable[i];
        }
        
        vizDescription = string.Format(abilityDescription,values);
    }
}
