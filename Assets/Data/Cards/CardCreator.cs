using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class CardCreator : MonoBehaviour
{
    
    public Sprite image;
    public CardLoader loader;
    public void Start()
    {

        CardData cardData = CreateCard();                                                                     //카드 데이터 제작

        string data = CardData.SerializeCardData(cardData);                                                         //데이터를 json 스트링으로 변환
        string path = Path.Combine(Application.dataPath + "/Data/Cards", cardData.title + ".json");        //json파일을 위치, 파일 이름으로 제작
        Debug.Log(data);
        Debug.Log(path);
        File.WriteAllText(path, data);                                                                                          //저장

        loader.cardName = cardData.title;
        loader.LoadData();

    }
    [ContextMenu("CreateCard")]
    public CardData CreateCard()
    {
        CardData cardData = new();


        cardData.title = "Barrier";
        cardData.imagePath = SpriteConverter.GetSpritePath(image);
        cardData.abilityDescription = "아군전체에 쉴드를 {0} 부여한다.";
        cardData.isNeedTarget = false;

        cardData.cardColor = ColorType.White;

        cardData.costs = new();
        cardData.costs.Add(ColorType.White, 2);

        cardData.cardAbility = new();
        TAll target = new TAll(0);

        GainShield shield = new GainShield(0, "Def",1);
        CardAbility ability = new(shield,target);
        cardData.cardAbility.Add(ability);
        


        return cardData;
    }

    
}
/*
 
        cardData.title = "Flame Shot";
        cardData.imagePath = SpriteConverter.GetSpritePath(image);
        cardData.abilityDescription = "대상에게 {0}의 피해를 입힌다.";
        cardData.isNeedTarget = true;

        cardData.cardColor = ColorType.Red;

        cardData.costs = new();
        cardData.costs.Add(ColorType.Red, 1);

        cardData.cardAbility = new();
        TTarget target = new TTarget();
        DealAndHeal dealAndHeal = new DealAndHeal(target, true, 5, 1, "Atk");
        Ability ability = new(dealAndHeal);
        cardData.cardAbility.Add(ability);
 


        cardData.title = "Poison";
        cardData.imagePath = SpriteConverter.GetSpritePath(image);
        cardData.abilityDescription = "대상에게 {0}의 맹독을 부여한다.";
        cardData.isNeedTarget = true;

        cardData.cardColor = ColorType.Blue;

        cardData.costs = new();
        cardData.costs.Add(ColorType.Blue, 1);

        cardData.cardAbility = new();
        TTarget target = new TTarget();
        Dot dot =new Dot();
        AddBuff addBuff = new AddBuff(dot, target, 3);
        Ability ability = new(addBuff);
        cardData.cardAbility.Add(ability);



        cardData.title = "WildFire";
        cardData.imagePath = SpriteConverter.GetSpritePath(image);
        cardData.abilityDescription = "대상에게 {0}의 피해를 입힌다. 이카드를 사용할 때마다 이번 전투동안 피해량이 3증가한다.";
        cardData.isNeedTarget = true;

        cardData.cardColor = ColorType.Red;

        cardData.costs = new();
        cardData.costs.Add(ColorType.Red, 1);

        cardData.cardAbility = new();
        TTarget target = new TTarget();
        DealAndHeal dealAndHeal = new DealAndHeal(true, 1, "Atk");
        EnhanceValue enhanceValue = new EnhanceValue(dealAndHeal, 3);
        
        Ability ability = new(enhanceValue,target);
        cardData.cardAbility.Add(ability);


        return cardData;



cardData.title = "Hemophillia";
        cardData.imagePath = SpriteConverter.GetSpritePath(image);
        cardData.abilityDescription = "대상에게 {0}의 피해를 입히고 출혈을 {1}부여한다.";
        cardData.isNeedTarget = true;

        cardData.cardColor = ColorType.Blue;

        cardData.costs = new();
        cardData.costs.Add(ColorType.Blue, 1);
        cardData.costs.Add(ColorType.Empty, 1);

        cardData.cardAbility = new();
        TTarget target = new TTarget();
        Bleed bleed = new Bleed();
        DealAndHeal deal =  new DealAndHeal(true,1,"Atk");
        AddBuff addBuff = new AddBuff(bleed, 2);

        Ability ability = new(addBuff,target), ability1 = new(deal,target);

        cardData.cardAbility.Add(ability);
        cardData.cardAbility.Add(ability1);


cardData.title = "AdditionalAttack";
        cardData.imagePath = SpriteConverter.GetSpritePath(image);
        cardData.abilityDescription = "대상에게 {0}의 피해를 부여된 독의 개수만큼 입힌다.";
        cardData.isNeedTarget = true;

        cardData.cardColor = ColorType.Blue;

        cardData.costs = new();
        cardData.costs.Add(ColorType.Blue, 2);

        cardData.cardAbility = new();
        TTarget target = new TTarget();
        
        DealAndHeal deal =  new DealAndHeal(true,1,"Atk");
        CheckBuff checkBuff = new CheckBuff(deal, false,typeof(Dot));

        Ability ability = new(checkBuff,target);

        cardData.cardAbility.Add(ability);


cardData.title = "Blossom";
        cardData.imagePath = SpriteConverter.GetSpritePath(image);
        cardData.abilityDescription = "대상에게 {0}의 피해를 입힌다. {1} : 적 전체에게 {2}의 피해를 입힌다.";
        cardData.isNeedTarget = true;

        cardData.cardColor = ColorType.Green;

        cardData.costs = new();
        cardData.costs.Add(ColorType.Green, 2);
        cardData.costs.Add(ColorType.Empty, 1);

        cardData.cardAbility = new();
        TTarget target = new TTarget();
        TAll all = new(1);

        DealAndHeal deal = new DealAndHeal(true, 1, "Atk");
        DealAndHeal deal1 = new DealAndHeal(true, 1, "Atk");
        CheckCost checkCost = new CheckCost(deal1, ColorType.Green, 5, true);
        Ability ability = new(deal, target);
        Ability ability2 = new(checkCost, all);
        cardData.cardAbility.Add(ability);
        cardData.cardAbility.Add(ability2);


cardData.title = "Berserk Soul";
        cardData.imagePath = SpriteConverter.GetSpritePath(image);
        cardData.abilityDescription = "자신에게 출혈을 {0}부여한다. {1} : 대상에게 {2}만큼 피해를 입힌다.";
        cardData.isNeedTarget = true;

        cardData.cardColor = ColorType.Black;

        cardData.costs = new();
        cardData.costs.Add(ColorType.Black, 1);

        cardData.cardAbility = new();
        TTarget target = new TTarget();

        Bleed bleed = new Bleed();
        AddBuff addBuff = new AddBuff(bleed, 2);
        DealDamagedReceived dealDamagedReceived = new DealDamagedReceived();
        CheckWound checkWound = new(dealDamagedReceived, false);
        Ability ability = new(addBuff);
        Ability ability1 = new(checkWound, target);
        cardData.cardAbility.Add(ability);
        cardData.cardAbility.Add(ability1);



cardData.title = "Stun";
        cardData.imagePath = SpriteConverter.GetSpritePath(image);
        cardData.abilityDescription = "대상에게 기절을 부여한다.";
        cardData.isNeedTarget = true;

        cardData.cardColor = ColorType.Blue;

        cardData.costs = new();
        cardData.costs.Add(ColorType.Blue, 2);

        cardData.cardAbility = new();
        TTarget target = new TTarget();

        Stun stun = new Stun();
        AddBuff addBuff = new AddBuff(stun, 1);
        Ability ability = new(addBuff,target);
        cardData.cardAbility.Add(ability);

cardData.title = "Shield";
        cardData.imagePath = SpriteConverter.GetSpritePath(image);
        cardData.abilityDescription = "대상에게 쉴드를 {0} 부여한다.";
        cardData.isNeedTarget = true;

        cardData.cardColor = ColorType.Green;

        cardData.costs = new();
        cardData.costs.Add(ColorType.Green, 1);

        cardData.cardAbility = new();
        TTarget target = new TTarget();

        GainShield shield = new GainShield(0, "Def",1);
        Ability ability = new(shield,target);
        cardData.cardAbility.Add(ability);
 */