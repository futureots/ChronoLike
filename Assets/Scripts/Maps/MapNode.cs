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
    public int mapNumber;                                //��,����,�޽�,����, ����
    public List<string> enemyData;                     //�� = �� ����Ʈ, ���� = ����, �޽� = ��ں�, ���� = ����, ���� = ����
    public List<point> nextNodes;                       //�������� �̵������� ����� ��ǥ
}
