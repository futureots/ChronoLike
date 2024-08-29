using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [System.Serializable]
    public struct stringResource
    {
        public string character;
        public List<string> cards;
    }
    [System.Serializable]
    public struct stringEnemyResource
    {
        public string character;
        public CharacterAI ai;
    }
    public List<stringResource> allies;
    public List<stringEnemyResource> enemies;
}
