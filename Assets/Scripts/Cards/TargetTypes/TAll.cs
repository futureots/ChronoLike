using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TAll : TargetType
{
    public int charSetCode;                                                                       //0 : �Ʊ� , 1 : ����, 2+ �׿� : ���δ�
    public TAll(int inCharSetCode)
    {
        charSetCode = inCharSetCode;

    }
    public override List<CharacterViz> GetTarget(bool isPlayerTeam)
    {
        List<CharacterViz> characters = new();
        characters.AddRange(GameManager.currentManager.characterManager.playableCharacters);
        characters.AddRange(GameManager.currentManager.characterManager.aiCharacters);
        List<CharacterViz> result = new();
        foreach (var item in characters)
        {
            switch (charSetCode)
            {
                case 0:
                    if (item.isAlly == isPlayerTeam) result.Add(item);
                    break;
                case 1:
                    if (item.isAlly != isPlayerTeam) result.Add(item);
                    break;
                default:
                    result.Add(item);
                    break;
            }
        }
        
        return result;
    }

}
