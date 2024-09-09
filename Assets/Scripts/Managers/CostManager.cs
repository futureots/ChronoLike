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
        
    }

    public void SetCostList(Dictionary<ColorType, int> costElementList)
    {
        foreach (var item in costElementList)
        {
            Status maxCost = Status.GetStatus(costList[(int)item.Key].costStatus, "MaxCost");
            if (maxCost != null)
            {
                maxCost.value = item.Value;
            }
            
        }
        FillCost();
    }
    public void FillCost()//�ڽ�Ʈ �г� ä��� ������Ʈ
    {
        for(int i = 0; i < costList.Count; i++)
        {
            //�ڽ�Ʈ ä���
            costList[i].RefillCost();
        }
    }

    public ColorType FindMostCost()//�������ִ� �ڽ�Ʈ �߿� ���� ���� ���� �ڽ�Ʈ �� ��ȯ
    {
        CostElement cost = costList[0];
        Status comparison = cost.costStatus[1];
        foreach (var item in costList)
        {
            Status currentCost = cost.costStatus[1];
            if (currentCost.value > comparison.value)
            {
                cost = item;
                comparison = cost.costStatus[1];
            }
            else if (currentCost.value == comparison.value)
            {
                Status maxComparison = cost.costStatus[0];
                Status maxCost = cost.costStatus[0];
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

        //����� �ڽ�Ʈ 1�� ����
        costList[costTypeNum].EditCost(-1);

        //�Ǹ��� �ڽ�Ʈ 1�� �߰�
        costCylinder.CreateSingleCost(costType);

        return;
    }
    public void ClearCylinder()// �Ǹ����� �ִ� �ڽ�Ʈ �ǵ�����
    {
        foreach(var item in costCylinder.colorCost)
        {
            costList[(int)item.Key].EditCost(item.Value);
        }
        costCylinder.ClearCylinder();
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
            
            
            Status currentCost = item.costStatus[1];
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

    


    
    public void FillCardCost(CardViz card=null)
    {
        if (card == null) return;      
        //�ڽ�ƮȮ�� �� ä���

        Dictionary<ColorType, int> lackOfCost = costCylinder.CheckCylinder(card.costs);
        FillCostCylinder(lackOfCost);

    }


    public bool ConsumeCost(CardViz cardViz)
    {
        Dictionary<ColorType, int> cost = cardViz.costs;
        bool canConsume = costCylinder.CheckConsumable(cost);
        Debug.Log("canConsume = " + canConsume);
        if (canConsume)
        {
            costCylinder.ConsumeCylinderCost(cardViz.costs);
        }
        return canConsume;
    }
    
    
}   
