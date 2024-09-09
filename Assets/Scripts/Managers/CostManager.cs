using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CostManager : MonoBehaviour
{
    public List<CostElement> costList;//남은 코스트

    public GameObject costPanel;//코스트 패널
    public CostCylinder costCylinder;//탭한 코스트

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
    public void FillCost()//코스트 패널 채우고 업데이트
    {
        for(int i = 0; i < costList.Count; i++)
        {
            //코스트 채우기
            costList[i].RefillCost();
        }
    }

    public ColorType FindMostCost()//가지고있는 코스트 중에 가장 많이 남은 코스트 색 반환
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


    public void ClickCost(int costTypeNum)//코스트1개 실린더에 채우기 무색코스트는 가지고 있는 코스트 중에가장많은색 코스트로 치환
    {
        
        //enum(costType)으로 변환
        ColorType costType = (ColorType)costTypeNum;

        //무색이면 가진코스트중에 가장 많은값으로 치환
        if(costType == ColorType.Empty)
        {
            costType = FindMostCost();
            costTypeNum = (int)costType;
        }

        //사용한 코스트 1개 차감
        costList[costTypeNum].EditCost(-1);

        //실린더 코스트 1개 추가
        costCylinder.CreateSingleCost(costType);

        return;
    }
    public void ClearCylinder()// 실린더에 있던 코스트 되돌리기
    {
        foreach(var item in costCylinder.colorCost)
        {
            costList[(int)item.Key].EditCost(item.Value);
        }
        costCylinder.ClearCylinder();
    }
    

    public bool FillCostCylinder(Dictionary<ColorType,int> cost)//실린더에 코스트만큼 채우기
    {
        //내 패널에 채울만큼 코스트가 남는지 확인
        if (!CheckFillable(cost))
        {
            Debug.Log("CanNot Fillable");
            return false;
        }
        //코스트만큼 패널 클릭
        foreach (var item in cost)
        {
            for(int i = 0; i < item.Value; i++)
            {
                ClickCost((int)item.Key);
            }
        }
        return true;

    }
    public bool CheckFillable(Dictionary<ColorType,int> values)//실린더에 필요 코스트만큼 채울 코스트가 남아있는지 확인한다.
    {
        //패널에 남은 코스트 딕셔너리화
        Dictionary<ColorType, int> remainCosts = new();
        foreach (var item in costList)
        {
            
            
            Status currentCost = item.costStatus[1];
            if (currentCost.value == 0) continue;
            remainCosts.Add(item.costType, currentCost.value);
        }
        //필요 코스트를 채울수 있으면 채우고 못채우면 false반환
        foreach (var item in values)
        {
            if (item.Key != ColorType.Empty)
            {
                if (!remainCosts.ContainsKey(item.Key)) return false;
                if (remainCosts[item.Key] < item.Value) return false;
                remainCosts[item.Key] -= item.Value;
            }
        }
        //오색 코스트 연산 후에 남은 코스트로 무색코스트 확인
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
        //코스트확인 후 채우기

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
