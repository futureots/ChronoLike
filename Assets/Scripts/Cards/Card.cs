using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    public TextMeshProUGUI title;
    public Image art;
    public TextMeshProUGUI describtion;
    public TextMeshProUGUI casterName;
    public Image cardBackground;
    public Transform resource;
    public GameObject costPrefab;
    public GameObject cardTemplate;
    //public CardData cardData;                                                              //ī�� ����
    public CharacterViz caster;                                                           //������

    public bool isNeedTarget;
    public int currentDeckNum;                                                            //���� ��ī�尡 �ִ� ���ѹ�
    public int discardNum,useNum;                                                         //������� �̵��ϴ� ���ѹ�, ���� �̵��ϴ� �� �ѹ�
    public ColorType cardColor;
    public List<CardCost> costs;
    public List<CardAbility> abilities;

    public Image raycastTarget { get; private set; }
    private void Awake()
    {
        raycastTarget = transform.GetChild(1).GetComponent<Image>();
    }
    public void LoadCard(CardData data, CharacterViz character)
    {
        gameObject.name = data.title;
        title.text = data.title;
        Sprite image = SpriteConverter.LoadSpriteFile(data.imagePath);
        art.sprite = image;
        caster = character;
        if (caster != null) casterName.text = character.name;

        isNeedTarget = data.isNeedTarget;
        cardColor = data.cardColor;
        costs = data.costs1;
        abilities = data.cardAbility;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
