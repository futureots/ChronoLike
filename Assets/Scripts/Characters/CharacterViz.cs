using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterViz : MonoBehaviour
{
    public Character character;
    public Image art;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI hp;
    public Image hpBar;

    public Transform buffTransform;
    public GameObject buffPrefab;


    private void Start()
    {
        LoadCharacter(character);
    }
    public void LoadCharacter(Character c)
    {
        if (c == null) return;
        character = c;
        if (c.data == null) return;
        art.sprite = c.art;
        characterName.text = c.name;

        Status currentHp = Status.GetStatus(c.statusList,"CurrentHp");
        Status maxHp = Status.GetStatus(c.statusList,"Hp");
        if (currentHp == null || maxHp == null) return;
        hp.text = currentHp.value + " / " + maxHp.value;
        hpBar.fillAmount = ((float)currentHp.value / maxHp.value);
    }
    public void UpdateCharacter()
    {
        Status currentHp = Status.GetStatus(character.statusList, "CurrentHp");
        Status maxHp = Status.GetStatus(character.statusList, "Hp");
        if (currentHp == null || maxHp == null) return;
        currentHp.EditValue(maxHp.value, Status.Operation.Fix, 1);
        hp.text = currentHp.value + " / " + maxHp.value;
        hpBar.fillAmount = ((float)currentHp.value / maxHp.value);
        for (int i = 0; i < character.buffs.Count; i++)
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
                viz.LoadBuffData(character.buffs[i]);
            }
        }
        for (int i = character.buffs.Count; i < buffTransform.childCount; i++)
        {
            buffTransform.GetChild(i).gameObject.SetActive(false);
        }

    }
    
}
