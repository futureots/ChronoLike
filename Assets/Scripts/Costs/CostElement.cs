using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ColorType
{
    Red = 0,
    Blue = 1,
    Green = 2,
    White = 3,
    Black = 4,
    Empty = 5
};


public class CostElement : MonoBehaviour
{
    public ColorType costType;
    public List<Status> costStatus;


    public Button button;
    public TextMeshProUGUI text;

    public void RefillCost()
    {
        Status currentCost = Status.GetStatus(costStatus, "CurrentCost");
        Status maxCost = Status.GetStatus(costStatus, "MaxCost");
        if (currentCost == null || maxCost ==null) return;
        currentCost.EditValue(maxCost.value, Status.Operation.Fix);
        CheckEmpty();
    }
    public void CheckEmpty()
    {
        Status currentCost = Status.GetStatus(costStatus, "CurrentCost");
        if (currentCost == null) return;
        Render();
        if (currentCost.value == 0) button.interactable = false;
        else button.interactable = true;
    }
    public void EditCost(int value = 1, bool isMaxCost=false)
    {
        Status cost;
        cost = costStatus[isMaxCost ? 0 : 1];
        if (cost == null) return;
        cost.EditValue(value, Status.Operation.Add);
        CheckEmpty();
    }
    public void Render()
    {
        int maxCost = Status.GetStatus(costStatus, "MaxCost").value;
        int currentCost = Status.GetStatus(costStatus, "CurrentCost").value;
        text.text = currentCost + "/" + maxCost;
    }






    [ContextMenu("setCost")]
    public void SetCost()
    {
        costStatus = new List<Status>();
        Status maxCost = new Status("MaxCost", 0, 10, 0);
        Status currentCost = new Status("CurrentCost", 0, 100, 0);
        costStatus.Add(maxCost);
        costStatus.Add(currentCost);
    }
}

