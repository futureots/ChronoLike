using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selectable : MonoBehaviour,IPointerClickHandler
{
    public SelectCardUI UI;
    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.transform.localScale = Vector3.one * 1.5f;
        UI.objectSelect(gameObject);
        Destroy(this);
    }

}
