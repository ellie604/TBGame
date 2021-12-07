using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : Singleton<UIMgr>
{
    [Header("UI����")]
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
        //��������ӽ�Dic
        foreach (var item in UIItemList)
        {
            UIItemDic.Add(item.GetComponent<BaseWidget>().WidgetName, item);
        }
        
        //����BackUpUI
        BackUpLayer.gameObject.SetActive(false);

        OpenPage("MainMenu");
    }

    /// <summary>
    /// ��ҳ��
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
            Debug.LogError("�ֵ���û�и�UI");
        }
    }

    /// <summary>
    /// �ر�ҳ��
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
            Debug.LogError("�ֵ���û�и�UI");
        }
    }

}
