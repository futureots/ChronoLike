using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;


public class MapMaker : MonoBehaviour
{
    List<List<bool>> mapShape;
    public GameObject canvas;
    public GameObject floorPrefab;
    public GameObject mapNodePrefab;
    
    void Start()
    {
        mapShape = new List<List<bool>>();
        List<bool> floor1  = new List<bool> { true,true,true,true,true};
        mapShape.Add(floor1);


        MapData mapData = new MapData();
        mapData.map = new();

        for (int i = 0; i < 10; i++)
        {
            List<bool> nextfloor = new List<bool>();

            List<MapNode> nodes = new();

            int length = 4;
            if (i > 5)
            {
                length = mapShape[i].Count - 1;
            }
            else if(mapShape[i].Count == 4)
            {
                length = 5;
            }
            for (int j = 0; j < length; j++) nextfloor.Add(false);


            string num = i + " = ";
            for (int j = 0; j < mapShape[i].Count; j++)
            {
                MapNode node = new();
                node.nextNodes = new();
                nodes.Add(node);
                if (mapShape[i][j])
                {
                    RandomStage(i, ref node);
                    if (mapShape[i].Count > nextfloor.Count)
                    {
                        if (j == 0)
                        {
                            nextfloor[j] = true;
                            node.nextNodes.Add(new point(i+1, j));
                        }
                        else if (j == mapShape[i].Count - 1)
                        {
                            nextfloor[j - 1] = true;
                            node.nextNodes.Add(new point(i+1, j-1));
                        }
                        else
                        {
                            nextfloor[j - 1] = nextfloor[j - 1] ? true : RandomBool(i);
                            nextfloor[j] = nextfloor[j - 1] ? RandomBool(i) : true;
                            node.nextNodes.Add(new point(i+1, j - 1));
                            node.nextNodes.Add(new point(i+1, j));
                        }
                    }
                    else
                    {
                        nextfloor[j] = nextfloor[j] ? true : RandomBool(i);
                        nextfloor[j + 1] = nextfloor[j] ? RandomBool(i) : true;
                        node.nextNodes.Add(new point(i+1, j));
                        node.nextNodes.Add(new point(i+1, j + 1));
                    }
                }
                else
                {
                    node.mapNumber = -1;
                }
                num += node.mapNumber + " ";
            }
            mapData.map.Add(nodes);
            mapShape.Add(nextfloor);
            Debug.Log(num);
        }
        List<MapNode> finalFloor = new();
        MapNode bossStage = new();
        bossStage.mapNumber = 1;
        finalFloor.Add(bossStage);
        mapData.map.Add(finalFloor);
        mapData.curNode = new(-1, 0);

        
        string data = MapData.SerializeCardData(mapData);                                                         //데이터를 json 스트링으로 변환
        string path = Path.Combine(Application.dataPath + "/Data/Maps", "Map.json");        //json파일을 위치, 파일 이름으로 제작
        Debug.Log(data);
        Debug.Log(path);
        File.WriteAllText(path, data);

    } 
    public bool RandomBool(int floor)
    {
        int i = Random.Range(0,100);
        int cut = 70;
        if (floor == 5 || floor > 6) return true;
        else if (floor == 6) cut = 50;
        if (i < cut) return false;
        else return true;

    }
    public void RandomStage(int floor, ref MapNode node)
    {
        node.mapNumber = Random.Range(0, 9);
    }

}
