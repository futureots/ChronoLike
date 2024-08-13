using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class SpriteConverter
{
    
    public static Sprite LoadSpriteFile(string inString)//파일경로받아오기
    {
        Sprite texture = (Sprite)AssetDatabase.LoadAssetAtPath(inString, typeof(Sprite));
        return texture;
    }
    public static string GetSpritePath(Sprite inSprite)
    {
        string imagePath="";
        string fileName = inSprite.name + ".*g";
        string[] path = Directory.GetFiles("Assets/Data/Sprites",fileName);
        foreach (var item in path)
        {
            imagePath = item;
        }
        return imagePath;
    }
}
