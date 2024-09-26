using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardCost : MonoBehaviour
{
    public CardViz card;
    public ColorType colorType;
    public int value;
    public TextMeshProUGUI text;
    public void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    protected virtual void Update()
    {
        text.text = value.ToString();
    }

}

