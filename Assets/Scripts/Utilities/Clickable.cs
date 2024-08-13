using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Clickable : MonoBehaviour, IPointerClickHandler
{
    public delegate void Clicked();
    public Clicked cardClicked;



    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        if (cardClicked != null) cardClicked();
    }
}
