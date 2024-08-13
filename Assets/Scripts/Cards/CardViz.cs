using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardViz : MonoBehaviour
{
    public Card card;

    public TextMeshProUGUI title;
    public Image art;
    public TextMeshProUGUI describtion;
    public TextMeshProUGUI casterName;
    public Image cardBackground;
    public Transform resource;
    public GameObject costPrefab;

    public GameObject cardTemplate;


    public CardData cardData { get; private set; }                                                              //카드 원본
    public Character caster;                                                           //시전자
    public Sprite image;
    public bool isNeedTarget;
    public Dictionary<ColorType, int> costs { get; private set; }
    public List<Ability> cardAbility;                                                   //카드 능력(키워드)
    public int discardNum;                                                      //카드 사용후 버려질지 소멸될지, 덱 번호값




    public Image raycastTarget { get; private set; }

    private void Awake()
    {
        raycastTarget = transform.GetChild(1).GetComponent<Image>();
    }
    public void LoadCard(CardData inCardData, Character inCaster=null)
    {
        if (inCardData == null) return;
        cardData = inCardData;
        gameObject.name = cardData.title;
        title.text = cardData.title;
        image = SpriteConverter.LoadSpriteFile(cardData.imagePath);
        art.sprite = image;
        caster = inCaster;
        if (caster != null)
        {
            casterName.text = caster.name;
        }
        
        isNeedTarget = cardData.isNeedTarget;
        costs = cardData.costs;
        cardAbility = cardData.cardAbility;
    }
    public void LoadCard(Card obj)
    {
        if (obj == null) return;
        card = obj;
        
        gameObject.name = card.title;                                                           //카드값 대입
        title.text = card.title;
        art.sprite = card.art;
        if(card.caster != null)
        {
            casterName.text = card.caster.name;
        }
        card.UpdateAbilityDescribtion();
        describtion.text = card.vizDescription;

        
        Color bgc = new();                                                                           //카드 배경색상 입히기
        switch (card.cardColor)
        {
            case ColorType.Red:
                bgc = Color.red;
                break;
            case ColorType.Blue:
                bgc = Color.blue;
                break;
            case ColorType.Green:
                bgc  = Color.green;
                break;
            case ColorType.White:
                bgc  = Color.white;
                break;
            case ColorType.Black:
                bgc = Color.black;
                break;
            case ColorType.Empty:
                break;
            default:
                break;
        }
        bgc *= 0.375f;
        bgc.a = 1;
        
        cardBackground.color = bgc;


        
        if (card.costs == null) return;                                                              //카드코스트 모형 만들기
        if (costPrefab == null || resource ==null) return;
        int i = 0;
        foreach (var item in card.costs)
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
            Image image = temp.GetComponent<Image>();
            TextMeshProUGUI cost = temp.GetComponentInChildren<TextMeshProUGUI>();
            cost.text = item.Value.ToString();

            switch ((int)item.Key)
            {
                case 0:
                    image.color = new Color(1, 0.25f, 0.25f);
                    cost.color = Color.white;
                    break;
                case 1:
                    image.color = new Color(0.25f, 0.25f, 1);
                    cost.color = Color.white;
                    break;
                case 2:
                    image.color = new Color(0.25f, 1, 0.25f);
                    cost.color = Color.black;
                    break;
                case 3:
                    image.color = new Color(1, 1, 1);
                    cost.color = Color.black;
                    break;
                case 4:
                    image.color = new Color(0.25f, 0.25f, 0.25f);
                    cost.color = Color.white;
                    break;
                case 5:
                    image.color = new Color(1, 1, 1, 0.25f);
                    cost.color = Color.black;
                    break;
            }

            
        }
    }

    
    public void UpdateAbility()
    {
        card.UpdateAbilityDescribtion();
        describtion.text = card.vizDescription;
    }
}