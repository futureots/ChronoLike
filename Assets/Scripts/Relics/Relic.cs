using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Relic : MonoBehaviour
{
    public Image image;
    
    public RelicData relicData;
    public void LoadRelic(RelicData inRelicData)
    {
        relicData = inRelicData;
        Sprite sprite = SpriteConverter.LoadSpriteFile(relicData.imagePath);
        image.sprite = sprite;
    }


    public void Activate()
    {
        foreach (var item in relicData.ability)
        {
            item.Activate();
        }
    }
}
