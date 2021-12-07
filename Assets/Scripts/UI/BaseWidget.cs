using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWidget : MonoBehaviour
{
    public string WidgetName;

    protected virtual void Awake()
    {
        //不修改则添加GameObject名字
        if (WidgetName == string.Empty)
        {
            WidgetName = gameObject.name;
        }
    }

    public abstract void OnOpenPage();

    public abstract void OnClosePage();

    public void SetToLayer(Transform targetLayer)
    {
        transform.SetParent(targetLayer);
        transform.SetAsLastSibling();
    }

    public void SetToLayer(GameObject UIObj, Transform targetLayer)
    {
        UIObj.transform.SetParent(targetLayer);
        UIObj.transform.SetAsLastSibling();
    }
}