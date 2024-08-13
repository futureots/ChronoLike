using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CostCylinder : MonoBehaviour
{
    public GameObject costPrefabs;

    public List<ColorType> cylinder;

    public Dictionary<ColorType, int> colorCost;

    private void Awake()
    {
        cylinder = new();
        colorCost = new();
    }
    public void CreateSingleCost(ColorType costType)
    {
        cylinder.Add(costType);

        if (!colorCost.ContainsKey(costType))
        {
            colorCost.Add(costType, 0);
        }
        colorCost[costType] += 1;

        GameObject temp = Instantiate(costPrefabs, transform);
        temp.name = "Cost" + costType;


        switch ((int)costType)
        {
            case 0:
                temp.GetComponent<Image>().color = new Color(1, 0.5f, 0.5f);
                break;
            case 1:
                temp.GetComponent<Image>().color = new Color(0.5f, 0.5f, 1);
                break;
            case 2:
                temp.GetComponent<Image>().color = new Color(0.5f, 1, 0.5f);
                break;
            case 3:
                temp.GetComponent<Image>().color = new Color(1, 1, 1);
                break;
            case 4:
                temp.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                break;
        }//색 입히기

        temp.GetComponentInChildren<TextMeshProUGUI>().text = costType.ToString();

        //실린더 크기 변경
        RectTransform  rect = GetComponent<RectTransform>();
        if (rect == null) return;
        int width = Mathf.Clamp(cylinder.Count, 6, 10);
        rect.sizeDelta = new Vector2(width * 80 + 20, rect.sizeDelta.y);
    }

    public void ClearCylinder()
    {
        while (cylinder.Count>0){
            RemoveCylinderCost(cylinder[0]);
        }
        Debug.Log("Finished");
        
    }

    
    public Dictionary<ColorType, int> CheckCylinder(Dictionary<ColorType, int> values)//매개변수 원본 넣어도 상관X
    {
        Dictionary<ColorType, int> temp = new();
        foreach (var item in values)
        {
            temp.Add(item.Key, item.Value);
        }

        foreach (var item in cylinder)
        {
            if (temp.ContainsKey(item))
            {
                temp[item] -= 1;
                if (temp[item] == 0)
                {
                    temp.Remove(item);
                }
            }
            else if(temp.ContainsKey(ColorType.Empty))
            {
                temp[ColorType.Empty] -= 1;
                if (temp[ColorType.Empty] == 0)
                {
                    temp.Remove(ColorType.Empty);
                }
            }
        }
        
        return temp;
    }
    public bool CheckConsumable(Dictionary<ColorType, int> values)
    {
        if (CheckCylinder(values).Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ConsumeCylinderCost(Dictionary<ColorType,int> values)
    {
        foreach (var item in values)
        {
            if (item.Key != ColorType.Empty)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    if (!RemoveCylinderCost(item.Key)) break;
                    
                }
            }
        }
        if (values.ContainsKey(ColorType.Empty))
        {
            for (int i = 0; i < values[ColorType.Empty]; i++)
            {
                if (!RemoveCylinderCost(ColorType.Empty)) break;
            }
        }

    }
    public bool RemoveCylinderCost(ColorType type)
    {
        if (type.Equals(ColorType.Empty))
        {
            if (cylinder.Count > 0)
            {

                colorCost[cylinder[0]] -= 1;
                cylinder.RemoveAt(0);

                GameObject temp = transform.GetChild(0).gameObject;
                temp.transform.SetParent(null);
                Destroy(temp);

                return true;
            }
            return false;

        }
        else
        {
            for (int i = 0; i < cylinder.Count; i++)
            {
                if (cylinder[i].Equals(type))
                {
                    colorCost[type] -= 1;
                    cylinder.RemoveAt(i);

                    GameObject temp = transform.GetChild(i).gameObject;
                    temp.transform.SetParent(null);
                    Destroy(temp);

                    return true;
                }
            }
            return false;
        }
    }
}
