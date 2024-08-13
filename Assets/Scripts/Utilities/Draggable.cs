using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    Transform canvas;
    public Transform previousParent { get; private set; }
    public int previousSiblingNum { get; private set; }
    RectTransform rect;
    public Image RaycastTarget;
    


    
    // Start is called before the first frame update
    protected void Awake()
    {
        canvas = transform.root;
        rect = GetComponent<RectTransform>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        previousParent = transform.parent;
        previousSiblingNum = transform.GetSiblingIndex();

        transform.SetParent(canvas);
        transform.SetAsLastSibling();
        if(RaycastTarget) RaycastTarget.raycastTarget = false;
        transform.localScale = Vector2.one * 1.5f;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 v = Camera.main.ScreenToWorldPoint(eventData.position);
        rect.position = v;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(transform.parent == canvas)
        {
            SetTransform();
        }
        Debug.Log("endDrag");
        
        
    }
    public void SetTransform()
    {
        transform.SetParent(previousParent);
        transform.SetSiblingIndex(previousSiblingNum);
        transform.localScale = Vector2.one;
        if (RaycastTarget) RaycastTarget.raycastTarget = true;
    }
}
