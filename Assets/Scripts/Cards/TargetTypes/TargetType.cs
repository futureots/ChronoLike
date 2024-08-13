using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetType
{
    public abstract List<Character> GetTarget(CharacterManager characterManager, bool isPlayerTeam);


}
