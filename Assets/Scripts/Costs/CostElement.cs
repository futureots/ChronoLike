using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ColorType
{
    Red = 0,
    Blue = 1,
    Green = 2,
    White = 3,
    Black = 4,
    Empty = 5
};


public class CostElement
{
    public ColorType costType;
    public List<Status> costStatus;
    public bool isZero;

    public CostElement(ColorType type,int basicCost=0)
    {
        costStatus = new();
        costType = type;
        Status maxCost = new("MaxCost", basicCost, 10, 0);
        Status currentCost = new("CurrentCost", basicCost, 100, 0);
        costStatus.Add(maxCost);
        costStatus.Add(currentCost);
        CheckEmpty();
    }

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

        if (currentCost.value == 0) isZero = true;
        else isZero = false;
    }
    public void EditCost(int value = 1)
    {
        Status currentCost = Status.GetStatus(costStatus, "CurrentCost");
        if (currentCost == null) return;
        currentCost.EditValue(value, Status.Operation.Add);
        CheckEmpty();
    }

}

