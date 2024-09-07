using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public struct TransformInfo
{
    public Transform parent;
    public int siblingIndex;
}
public class SelectCardUI : MonoBehaviour
{
    public Transform objectsTransform;

    public delegate void SelectedObjectsCommands(GameObject obj);
    public SelectedObjectsCommands selectedObjectsCommands;

    public Dictionary<GameObject, TransformInfo> prevObjectsTransform;
    public List<GameObject> selectedObjects;
    public int count;

    public void SetObjects(List<GameObject> objects,int num,SelectedObjectsCommands Command=null)
    {
        count = num;
        selectedObjects = new();
        prevObjectsTransform = new();
        foreach (var item in objects) 
        {
            TransformInfo info = new TransformInfo();
            info.parent = item.transform.parent;
            info.siblingIndex = item.transform.GetSiblingIndex();
            prevObjectsTransform.Add(item, info);
        }
        foreach (var item in objects)
        {
            item.transform.SetParent(objectsTransform);
            item.transform.localScale = Vector3.one;
            Selectable selectable = item.AddComponent<Selectable>();
            selectable.UI = this;
        }
        selectedObjectsCommands = Command;
    }
    public void objectSelect(GameObject obj)
    {
        selectedObjects.Add(obj);
        if (selectedObjects.Count >= count)
        {
            ExecuteCommands();
            gameObject.SetActive(false);
        }
        
    }

    public void ExecuteCommands()
    {
        if (selectedObjects == null) return;
        if (selectedObjectsCommands == null) return;
        foreach (var item in prevObjectsTransform)
        {
            Selectable selectable = item.Key.GetComponent<Selectable>();
            if (selectable != null)
            {
                Destroy(selectable);
            }

            item.Key.transform.SetParent(item.Value.parent);
            item.Key.transform.SetSiblingIndex(item.Value.siblingIndex);
            
        }
        prevObjectsTransform.Clear();
        foreach (var item in selectedObjects)
        {
            Debug.Log(item.name + " active =>" + selectedObjectsCommands.Method.Name);
            selectedObjectsCommands(item);
            item.transform.localScale = Vector3.one;
        }
        selectedObjects.Clear();

    }
}
