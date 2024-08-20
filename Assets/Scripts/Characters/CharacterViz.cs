using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterViz : MonoBehaviour
{
    public Image art;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI hp;
    public Image hpBar;

    public Transform buffTransform;
    public GameObject buffPrefab;


    public CharacterData data;
    public List<Status> statusList;
    public ColorType colorType;
    public Dictionary<string, Ability> characterAbility;
    public List<Buff> buffs;
    public bool isAlly;




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
        buffs = new();
        colorType = inCharData.type;

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
        for (int i = 0; i < buffs.Count; i++)
        {
            GameObject buffViz;
            if (buffTransform.childCount > i)
            {
                buffViz = buffTransform.GetChild(i).gameObject;
                buffViz.SetActive(true);
            }
            else buffViz = Instantiate(buffPrefab, buffTransform);
            BuffViz viz = buffViz.GetComponent<BuffViz>();
            if (viz != null)
            {
                viz.LoadBuffData(buffs[i]);
            }
        }
        for (int i = buffs.Count; i < buffTransform.childCount; i++)
        {
            buffTransform.GetChild(i).gameObject.SetActive(false);
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
        if (currentHp.StatIsZero())
        {
            Dead();
        }
    }
    public void Dead()
    {

    }

    public void AttachBuff(Buff inBuff)
    {
        foreach (var item in buffs)
        {
            if (item.MergeBuff(inBuff))
            {
                return;
            }

        }
        //inBuff.AttachBuff(this);
    }
}
