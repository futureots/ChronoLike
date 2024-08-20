using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI : MonoBehaviour
{
    
    public CharacterViz character;
    public int actionCount;

    public enum ActionType
    {
        Attack,
        Buff,
        DeBuff,
        Heal,
        Shield
    }

    
    private void Awake()
    {
        
    }

    
}
