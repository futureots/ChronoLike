using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class MapLoader : MonoBehaviour
{
    public GameObject canvas;
    public GameObject nodePrefab;
    public GameObject floorPrefab;
    MapData mapData;
    List<List<Button>> nodeBtns;
    private void Start()
    {
        LoadMap();
    }

    public void LoadMap()
    {
        string path = Path.Combine(Application.dataPath + "/Data/Maps",  "Map.json");
        string data = null;
        if (File.Exists(path))
        {
            data = File.ReadAllText(path);
        }
        mapData = MapData.DeserializeCardData(data);

        nodeBtns = new List<List<Button>>();
        foreach (var floor in mapData.map)
        {
            GameObject floorObj = Instantiate(floorPrefab, canvas.transform);
            List<Button> buttonList = new List<Button>();
            foreach (var stage in floor)
            {
                GameObject nodeObj = Instantiate(nodePrefab, floorObj.transform);
                Button nodeBtn = nodeObj.GetComponent<Button>();
                nodeBtn.onClick.AddListener(() =>
                {
                    foreach (var item in buttonList)
                    {
                        item.interactable = false;
                    }
                    if (stage.nextNodes == null)
                    {
                        Debug.Log("EndOfFloor");
                        return;
                    }
                    foreach (var item in stage.nextNodes)
                    {
                        SetEnableNode(item.y, item.x);
                    }
                });
                buttonList.Add(nodeBtn);
                if (stage.mapNumber!=-1)
                {
                    nodeObj.GetComponentInChildren<TextMeshProUGUI>().text = stage.mapNumber.ToString();
                }
                nodeObj.GetComponent<Button>().interactable = false;

            }
            nodeBtns.Add(buttonList);
        }
        foreach (var item in nodeBtns[0])
        {
            item.interactable = true;
        }
        
    }
    public void SetEnableNode(int floor, int stageNum)
    {
        if(mapData.map[floor][stageNum].mapNumber !=-1) nodeBtns[floor][stageNum].interactable = true;
        else nodeBtns[floor][stageNum].interactable = false;
    }

}
