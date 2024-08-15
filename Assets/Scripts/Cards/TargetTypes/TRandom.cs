using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TRandom : TargetType
{
    public int num;
    public int charSetCode;                                                                       //0 : 아군 , 1 : 적군, 2+ 그외 : 전부다
    public TRandom(int inNum, int inCharSetCode)
    {
        num = inNum;
        charSetCode = inCharSetCode;
    }
    public override List<Character> GetTarget(bool isPlayerTeam)//중복 없이 랜덤 인원 뽑기(중복이 필요하면 프로퍼티 두개 사용)
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
