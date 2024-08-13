using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Status
{
    public string name;
    public int max;
    public int min;
    public int value;

    public Status()
    {

    }

    public enum Operation
    {
        Add=0,
        Multiple=1,
        Fix=2
    }
    public Status(string inName, int inValue,int inMax, int inMin)
    {
        name = inName;
        value = inValue;
        max = inMax;
        min = inMin;
    }
    public Status(Status copy)
    {
        this.name = copy.name;
        this.value = copy.value;
        this.max = copy.max;
        this.min = copy.min;
    }
    public void EditValue(int edit,Operation operation, int code=0)//code = 0 : value, 1 : max, 2 : min
    {
        int editor;
        switch (code)
        {
            case 1:
                editor = max;
                break;
            case 2:
                editor = min;
                break;
            default:
                editor = value;
                break;
        }

        switch (operation)
        {
            case Operation.Add:
                editor += edit;
                break;
            case Operation.Multiple:
                editor *= edit;
                break;
            case Operation.Fix:
                editor = edit;
                break;
        }
        
        switch (code)
        {
            case 1:
                max = editor;
                break;
            case 2:
                min = editor;
                break;
            default:
                value = Mathf.Clamp(editor, min, max);
                break;
        }
        
    }

    public static Status GetStatus(List<Status> list,string name)
    {
        foreach (var item in list)
        {
            if (item.name.Equals(name))
            {
                return item;
            }
        }
        return null;
    }
}
