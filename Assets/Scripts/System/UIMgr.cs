using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : Singleton<UIMgr>
{
    [Header("UI引用")]
    public GameObject[] UIItemList = new GameObject[] { };
    public Dictionary<string, GameObject> UIItemDic = new Dictionary<string, GameObject>();

    [Header("Ref")]
    public Transform CanvasRoot;
    public Transform Layer1;
    public Transform Layer2;
    public Transform BackUpLayer;

    protected override void Awake()
    {
        base.Awake();
        if(CanvasRoot==null)
        {
            CanvasRoot = transform;
        }
    }

    private void Start()
    {
        //将数组添加进Dic
        foreach (var item in UIItemList)
        {
            UIItemDic.Add(item.GetComponent<BaseWidget>().WidgetName, item);
        }
        
        //隐藏BackUpUI
        BackUpLayer.gameObject.SetActive(false);

        OpenPage("MainMenu");
    }

    /// <summary>
    /// 打开页面
    /// </summary>
    /// <param name="UIName"></param>
    public void OpenPage(string UIName)
    {
        if(UIItemDic.ContainsKey(UIName))
        {
            BaseWidget widget = UIItemDic[UIName].GetComponent<BaseWidget>();
            widget.SetToLayer(Layer1);
            widget.OnOpenPage();
        }
        else
        {
            Debug.LogError("字典里没有该UI");
        }
    }

    /// <summary>
    /// 关闭页面
    /// </summary>
    /// <param name="UIName"></param>
    public void ClosePage(string UIName)
    {
        if (UIItemDic.ContainsKey(UIName))
        {
            BaseWidget widget = UIItemDic[UIName].GetComponent<BaseWidget>();
            widget.SetToLayer(BackUpLayer);
            widget.OnClosePage();
        }
        else
        {
            Debug.LogError("字典里没有该UI");
        }
    }

}
