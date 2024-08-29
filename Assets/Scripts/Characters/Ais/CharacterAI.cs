using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterAI : MonoBehaviour
{
    
    protected CharacterViz character;
    public delegate void Action(CharacterViz target);
    public Action action;
    public Image panelImage;
    public TextMeshProUGUI panelText;
    public TextMeshProUGUI TargetText;
    public string iconPath = "Assets/Data/Sprites\\";

    protected List<CharacterViz> targets;
    public void SetAI(CharacterViz inCharacter)
    {
        character = inCharacter;
        this.transform.SetParent(inCharacter.transform);

    }
    private void Awake()
    {
        panelImage = GetComponent<Image>();
        panelText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TargetText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }
    public virtual void SetAction()                                                             //공격 방식(ex : 일정체력 이하, 몇턴 경과, 특정 기믹) /타겟지정도 여기서
    {
        Debug.Log("Action Set");
        if (action != null)
        {
            action = null;
            RenderPanel(null, "");
        }
        else
        {
            TRandom tRandom = new TRandom(1);
            targets = tRandom.GetTarget(character.isAlly);
            if (targets.Count == 0)
            {
                action = null;
                RenderPanel(null, "");
            }
            action = new Action(Attack);
            Sprite icon = SpriteConverter.LoadSpriteFile(iconPath + "GreatSword.png");
            Status atk = Status.GetStatus(character.statusList, "Atk");
            Debug.Log(targets.Count);
            RenderPanel(icon, atk.value.ToString(), targets);
        }

    }
    public void CharAction()
    {
        if (action == null) return;
        Debug.Log(character+ "Action");
        bool actable = character.isActable;
        character.ActBefore.Invoke();
        if (actable)
        {
            foreach (var target in targets)
            {
                action.Invoke(target);
                
            }
            character.ActAfter.Invoke();
            targets.Clear();
        }
    }

    
    public void Attack(CharacterViz target)
    {
        Status atk = Status.GetStatus(character.statusList, "Atk");
        //Debug.Log("Enemy Attack");
        target.Damaged(atk.value);
        
    }

    public void RenderPanel(Sprite image, string text,List<CharacterViz> inTarget= null)
    {
        if(image == null)
        {
            panelImage.color = Color.clear;
        }
        else
        {
            panelImage.color = Color.white;
            panelImage.sprite = image;
        }
        panelText.text = text;
        if(inTarget == null)
        {
            TargetText.text = "";
        }
        else
        {
            int count  = inTarget.Count-1;
            TargetText.text = inTarget[0].data.name;
            if (count > 0) TargetText.text += " + " + count;
        }
    }
}
