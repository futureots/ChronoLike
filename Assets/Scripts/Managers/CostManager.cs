using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CostManager : MonoBehaviour
{
    public List<CostElement> costList;//���� �ڽ�Ʈ

    public GameObject costPanel;//�ڽ�Ʈ �г�
    public CostCylinder costCylinder;//���� �ڽ�Ʈ

    private void Awake()
    {
        costList = new();
    }

    public void SetCostList(List<CostElement> costElementList)
    {
        costList.AddRange(costElementList);
        FillCost();
    }
    public void FillCost()//�ڽ�Ʈ �г� ä��� ������Ʈ
    {
        for(int i = 0; i < costList.Count; i++)
        {
            //�ڽ�Ʈ ä���
            costList[i].RefillCost();

            //UI������
            RenderCost(i);
        }
    }

    public ColorType FindMostCost()//�������ִ� �ڽ�Ʈ �߿� ���� ���� ���� �ڽ�Ʈ �� ��ȯ
    {
        CostElement cost = costList[0];
        Status comparison = Status.GetStatus(cost.costStatus, "CurrentCost");
        foreach (var item in costList)
        {
            Status currentCost = Status.GetStatus(item.costStatus, "CurrentCost");
            if (currentCost.value > comparison.value)
            {
                cost = item;
                comparison = Status.GetStatus(cost.costStatus, "CurrentCost");
            }
            else if (currentCost.value == comparison.value)
            {
                Status maxComparison = Status.GetStatus(cost.costStatus, "MaxCost");
                Status maxCost = Status.GetStatus(item.costStatus, "MaxCost");
                if (maxCost.value>maxComparison.value)
                {
                    cost = item;
                }
            }
        }
        return cost.costType;
    }


    public void ClickCost(int costTypeNum)//�ڽ�Ʈ1�� �Ǹ����� ä��� �����ڽ�Ʈ�� ������ �ִ� �ڽ�Ʈ �߿����帹���� �ڽ�Ʈ�� ġȯ
    {
        
        //enum(costType)���� ��ȯ
        ColorType costType = (ColorType)costTypeNum;

        //�����̸� �����ڽ�Ʈ�߿� ���� ���������� ġȯ
        if(costType == ColorType.Empty)
        {
            costType = FindMostCost();
            costTypeNum = (int)costType;
        }

        if (costList[costTypeNum].isZero) return;
        //����� �ڽ�Ʈ 1�� ����
        costList[costTypeNum].EditCost(-1);

        //�Ǹ��� �ڽ�Ʈ 1�� �߰�
        costCylinder.CreateSingleCost(costType);

        //���� �ڽ�Ʈ ǥ��
        RenderCost(costTypeNum);
        return;
    }
    public void ClearCylinder()// �Ǹ����� �ִ� �ڽ�Ʈ �ǵ�����
    {
        foreach(var item in costCylinder.colorCost)
        {
            costList[(int)item.Key].EditCost(item.Value);
            RenderCost((int)item.Key);
        }
        costCylinder.ClearCylinder();
    }
    public void RenderCost(int typeNum)//�ڽ�Ʈ UI ������Ʈ
    {
        GameObject costBtn = costPanel.transform.GetChild(typeNum).gameObject;
        costBtn.GetComponent<Button>().interactable = !costList[typeNum].isZero;

        Status currentCost = Status.GetStatus(costList[typeNum].costStatus, "CurrentCost");
        Status maxCost = Status.GetStatus(costList[typeNum].costStatus, "MaxCost");

        string costText = currentCost.value.ToString() +"/" + maxCost.value.ToString();
        costPanel.transform.GetChild(typeNum).GetComponentInChildren<TextMeshProUGUI>().text = costText;
    }
    

    public bool FillCostCylinder(Dictionary<ColorType,int> cost)//�Ǹ����� �ڽ�Ʈ��ŭ ä���
    {
        //�� �гο� ä�︸ŭ �ڽ�Ʈ�� ������ Ȯ��
        if (!CheckFillable(cost))
        {
            Debug.Log("CanNot Fillable");
            return false;
        }
        //�ڽ�Ʈ��ŭ �г� Ŭ��
        foreach (var item in cost)
        {
            for(int i = 0; i < item.Value; i++)
            {
                ClickCost((int)item.Key);
            }
        }
        return true;

    }
    public bool CheckFillable(Dictionary<ColorType,int> values)//�Ǹ����� �ʿ� �ڽ�Ʈ��ŭ ä�� �ڽ�Ʈ�� �����ִ��� Ȯ���Ѵ�.
    {
        //�гο� ���� �ڽ�Ʈ ��ųʸ�ȭ
        Dictionary<ColorType, int> remainCosts = new();
        foreach (var item in costList)
        {
            Status currentCost = Status.GetStatus(item.costStatus, "CurrentCost");
            if (currentCost.value == 0) continue;
            remainCosts.Add(item.costType, currentCost.value);
        }
        //�ʿ� �ڽ�Ʈ�� ä��� ������ ä��� ��ä��� false��ȯ
        foreach (var item in values)
        {
            if (item.Key != ColorType.Empty)
            {
                if (!remainCosts.ContainsKey(item.Key)) return false;
                if (remainCosts[item.Key] < item.Value) return false;
                remainCosts[item.Key] -= item.Value;
            }
        }
        //���� �ڽ�Ʈ ���� �Ŀ� ���� �ڽ�Ʈ�� �����ڽ�Ʈ Ȯ��
        if (values.ContainsKey(ColorType.Empty))
        {
            int n = values[ColorType.Empty];
            foreach (var v in remainCosts)
            {
                n -= v.Value;
                if (n <= 0) return true;
            }
            if (n > 0) return false;
        }
        return true;
    }

    


    
    public void FillCardCost(Card card=null)
    {
        if (card == null) return;      
        //�ڽ�ƮȮ�� �� ä���

        Dictionary<ColorType, int> lackOfCost = costCylinder.CheckCylinder(card.costs);
        FillCostCylinder(lackOfCost);

    }


    public bool ConsumeCost(Card card)
    {
        Dictionary<ColorType, int> cost = card.costs;
        bool canConsume = costCylinder.CheckConsumable(cost);
        if (canConsume)
        {
            costCylinder.ConsumeCylinderCost(card.costs);
        }
        return canConsume;
    }
    
    
}   
