using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardViz : MonoBehaviour
{

    public TextMeshProUGUI title;
    public Image art;
    public TextMeshProUGUI describtion;
    public TextMeshProUGUI casterName;
    public Image cardBackground;
    public Transform resource;
    public GameObject costPrefab;

    public GameObject cardTemplate;


    public CardData cardData;                                                              //카드 원본
    public CharacterViz caster;                                                           //시전자
    
    public bool isNeedTarget;
    public Dictionary<ColorType, int> costs { get; private set; }
    public List<Ability> cardAbility;                                                   //카드 능력(키워드)
    public int discardNum;                                                      //카드 사용후 버려질지 소멸될지, 덱 번호값




    public Image raycastTarget { get; private set; }

    private void Awake()
    {
        raycastTarget = transform.GetChild(1).GetComponent<Image>();
    }
    public void LoadCard(CardData inCardData, CharacterViz inCaster=null)
    {
        if (inCardData == null) return;
        cardData = inCardData;
        gameObject.name = cardData.title;
        title.text = cardData.title;
        Sprite image = SpriteConverter.LoadSpriteFile(cardData.imagePath);
        art.sprite = image;
        caster = inCaster;
        if (caster != null)
        {
            casterName.text = caster.name;
        }
        
        isNeedTarget = cardData.isNeedTarget;
        costs = cardData.costs;
        cardAbility = cardData.cardAbility;
        UpdateAbilityDescribtion();

        Color bgc = new();
        switch (cardData.cardColor)
        {
            case ColorType.Red:
                bgc = Color.red;
                break;
            case ColorType.Blue:
                bgc= Color.blue;
                break;
            case ColorType.Green:
                bgc= Color.green;
                break;
            case ColorType.White:
                bgc= Color.white;
                break;
            case ColorType.Black:
                bgc= Color.black;
                break;
            case ColorType.Empty:
                bgc = Color.white * 0.5f;
                bgc.a *= 0.5f;
                break;
            default:
                break;
        }
        cardBackground.color = bgc;


        if (costs == null) return;                                                              //카드코스트 모형 만들기
        if (costPrefab == null || resource == null) return;
        int i = 0;
        foreach (var item in costs)
        {
            GameObject temp;
            if (resource.childCount > i)
            {
                temp = resource.GetChild(i).gameObject;
            }
            else
            {
                temp = Instantiate(costPrefab, resource);
            }
            i++;


            temp.name = item.Key.ToString();                                                            //색 입히기
            Image costColor = temp.GetComponent<Image>();
            TextMeshProUGUI cost = temp.GetComponentInChildren<TextMeshProUGUI>();
            cost.text = item.Value.ToString();

            switch ((int)item.Key)
            {
                case 0:
                    costColor.color = new Color(1, 0.25f, 0.25f);
                    cost.color = Color.white;
                    break;
                case 1:
                    costColor.color = new Color(0.25f, 0.25f, 1);
                    cost.color = Color.white;
                    break;
                case 2:
                    costColor.color = new Color(0.25f, 1, 0.25f);
                    cost.color = Color.black;
                    break;
                case 3:
                    costColor.color = new Color(1, 1, 1);
                    cost.color = Color.black;
                    break;
                case 4:
                    costColor.color = new Color(0.25f, 0.25f, 0.25f);
                    cost.color = Color.white;
                    break;
                case 5:
                    costColor.color = new Color(1, 1, 1, 0.25f);
                    cost.color = Color.black;
                    break;
            }


        }
    }

    public void UpdateAbilityDescribtion()
    {
        string vizDescription;
        if (cardAbility.Count <= 0) return;
        List<string> abilityVariable = new();
        foreach (var item in cardAbility)
        {
            //Debug.Log(item.effect.GetVariables(caster).Count);
            abilityVariable.AddRange(item.effect.GetVariables(caster));
        }
        string[] values = new string[abilityVariable.Count];
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = abilityVariable[i];
        }
        //Debug.Log(cardData.abilityDescription + "//" + values.Length);
        vizDescription = string.Format(cardData.abilityDescription, values);
        describtion.text = vizDescription;
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
}