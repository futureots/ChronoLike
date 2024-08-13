using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Droppable : MonoBehaviour,IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        //CardViz viz = eventData.pointerDrag.GetComponent<CardViz>();

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        
        if (eventData.pointerDrag == null) return;
        //Debug.Log(gameObject.name+"Hitted");
        GameManager.currentManager.UsePlayerCard(eventData.pointerDrag, gameObject);
    }

}
