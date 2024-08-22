﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterViz : MonoBehaviour
{
    public Image art;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI hp;
    public bool isActable;
    public Image hpBar;

    public Transform buffTransform;
    public List<Buff> buffList;
    public GameObject buffPrefab;
    

    public CharacterData data;
    public List<Status> statusList;
    public ColorType colorType;
    public Dictionary<string, Ability> characterAbility;
    
    public bool isAlly;

    public delegate void AbilityActivate();
    public AbilityActivate TurnStart, TurnEnd, CharAction, CharDamaged, CharDead;

    private void Awake()
    {
        TurnStart = new AbilityActivate(() => { });
        TurnEnd = new AbilityActivate(() => { });
        CharAction = new AbilityActivate(() => { });
        CharDamaged = new AbilityActivate(() => { });
        CharDead = new AbilityActivate(() => { });
    }
    public void LoadCharacter(CharacterData inCharData, bool inIsAlly)
    {
        if (inCharData == null) return;
        data = inCharData;
        characterName.text = inCharData.name;
        gameObject.name = inCharData.name;
        Sprite image = SpriteConverter.LoadSpriteFile(inCharData.artPath);
        art.sprite = image;
        isAlly = inIsAlly;
        characterAbility = data.characterAbility;
        buffList = new();
        colorType = inCharData.type;
        isActable = true;
        statusList = new List<Status>();
        foreach (var item in inCharData.statusList)
        {
            statusList.Add(new Status(item));
        }
        Status currentHp = new Status(Status.GetStatus(statusList, "Hp"));
        currentHp.name = "CurrentHp";
        statusList.Add(currentHp);
        UpdateCharacter();
    }

    public void UpdateCharacter()
    {
        Status currentHp = Status.GetStatus(statusList, "CurrentHp");
        Status maxHp = Status.GetStatus(statusList, "Hp");
        if (currentHp == null || maxHp == null) return;
        currentHp.EditValue(maxHp.value, Status.Operation.Fix, 1);
        hp.text = currentHp.value + " / " + maxHp.value;
        hpBar.fillAmount = ((float)currentHp.value / maxHp.value);

        BuffViz viz = null;
        for(int i = 0; i < buffTransform.childCount; i++)
        {
            viz = buffTransform.GetChild(i).GetComponent<BuffViz>();
            if (viz != null)
            {
                
                int num = viz.UpdateViz();
                //Debug.Log(viz.buff.target + "// " + num);
                if (num == 0)
                {
                    //캐릭터 버프 제거
                    viz.buff.DetachBuff();
                    buffList.Remove(viz.buff);
                    //버프 오브젝트 제거
                    viz.transform.SetParent(null);                                                           //나중에 코루틴 쓰게되면 제거 요망
                    Destroy(viz.gameObject);
                }
            }
        }
    }
    public void EditCharacter(string name, int value, Status.Operation operation, int code = 0)//code = 1: max, 2 :min, 그 외 : value
    {
        foreach (var item in statusList)
        {
            if (item.name.Equals(name))
            {
                item.EditValue(value, operation, code);
            }
        }
    }

    
    public void Damaged(int damage)
    {
        Status currentHp = Status.GetStatus(statusList, "CurrentHp");
        currentHp.EditValue(-damage, Status.Operation.Add);
        CharDamaged();
        if (currentHp.StatIsZero())
        {
            Dead();
        }
    }
    public void Dead()
    {
        CharDead.Invoke();
    }

    public void AttachBuff(Buff inBuff)
    {
        Buff buff = inBuff.Clone();
        BuffViz buffViz;
        for(int i=0;i<buffList.Count;i++)
        {
            if (buffList[i].MergeBuff(buff))
            {
                buffViz = buffTransform.GetChild(i).GetComponent<BuffViz>();
                buffViz.UpdateViz();
                return;
            }

        }
        buffViz = Instantiate(buffPrefab, buffTransform).GetComponent<BuffViz>();
        //Debug.Log(buffViz.name);
        buffViz.LoadBuffData(buff);
        buffList.Add(buff);
        buff.Init(this);

    }

}
