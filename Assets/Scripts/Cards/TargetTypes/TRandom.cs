using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TRandom : TargetType
{
    public int num;
    public int charSetCode;                                                                       //0 : �Ʊ� , 1 : ����, 2+ �׿� : ���δ�
    public TRandom(int inNum, int inCharSetCode)
    {
        num = inNum;
        charSetCode = inCharSetCode;
    }
    public override List<Character> GetTarget(bool isPlayerTeam)//�ߺ� ���� ���� �ο� �̱�(�ߺ��� �ʿ��ϸ� ������Ƽ �ΰ� ���)
    {
        List<Character> characters = new();
        characters.AddRange(GameManager.currentManager.characterManager.playableCharacters);
        characters.AddRange(GameManager.currentManager.characterManager.aiCharacters);
        List<Character> pool = new();
        foreach (var item in characters)
        {
            switch (charSetCode)
            {
                case 0:
                    if(item.isAlly ==isPlayerTeam) pool.Add(item);
                    break;
                case 1:
                    if (item.isAlly != isPlayerTeam) pool.Add(item);
                    break;
                default:
                    pool.Add(item);
                    break;
            }
        }

        List<Character> results = new();
        for (int i = 0; i < num; i++)
        {
            if (pool.Count > 0)
            {
                int index = Random.Range(0, pool.Count);
                results.Add(pool[index]);
                pool.RemoveAt(index);
            }
            else
            {
                Debug.Log("No More RandomCharacter");
            }
        }
        return results;
    }
}
