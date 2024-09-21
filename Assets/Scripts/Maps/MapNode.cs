using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public struct point
{
    public point(int inY, int inX)
    {
        y= inY;
        x= inX;
    }
    public int y;
    public int x;
}
public class MapNode
{
    public int mapNumber;                                //적,상점,휴식,보물, 보스
    public List<string> enemyData;                     //적 = 적 리스트, 상점 = 상인, 휴식 = 모닥불, 보물 = 상자, 보스 = 보스
    public List<point> nextNodes;                       //다음으로 이동가능한 노드의 좌표
}
