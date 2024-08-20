using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CardLoader : MonoBehaviour
{
    public string cardName;
    CardData cardData;
    public CardViz viz;
    private void Start()
    {
        
        
    }

    public void LoadData()
    {
        string path = Path.Combine(Application.dataPath + "/Data/Cards", cardName + ".json");
        string data = null;
        if (File.Exists(path))
        {
            data = File.ReadAllText(path);
        }


        cardData = CardData.DeserializeCardData(data);
        viz.LoadCard(cardData);
    }
}
