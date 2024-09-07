using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyWordInKeyWord : KeyWord
{
    public KeyWord keyWord;
    public override void SetKeyword(CharacterViz inCaster)
    {
        keyWord.SetKeyword(inCaster);
        base.SetKeyword(inCaster);
    }
    public override void Active(CardViz cardViz, CharacterViz target)
    {
        
    }
}
