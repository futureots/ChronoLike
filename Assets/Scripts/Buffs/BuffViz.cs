using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuffViz : MonoBehaviour
{
    public Buff buff;

    public TextMeshProUGUI buffCount;
    public Image buffIcon;

    public CharacterViz target;
    public int countNum;


    public void LoadBuffData(Buff inBuff)
    {
        if (inBuff == null) return;
        buff = inBuff;
        buffCount.text = buff.countNum.ToString();
        Sprite icon = SpriteConverter.LoadSpriteFile(buff.iconPath);
        buffIcon.sprite = icon;
    }

    public int UpdateViz()
    {
        
        buffCount.text = buff.countNum.ToString();
        return buff.countNum;
    }
}
