using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public List<PoolObject> poolObjects = new List<PoolObject>();

    public static PoolManager instance;

    private Dictionary<string, Queue<GameObject>> poolDic = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        #region 单例
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        DontDestroyOnLoad(gameObject);
        # endregion
    }

    private void Start()
    {
        //填充字典
        SetUpPool();

    }

    [ContextMenu("测试初始化")]
    /// <summary>
    /// 根据预设初始化对象池
    /// </summary>
    public void SetUpPool()
    {
        foreach (PoolObject obj in poolObjects)
        {
            Queue<GameObject> newQueue = new Queue<GameObject>();

            for (int i = 0; i < obj.size; i++)
            {
                GameObject newObject = Instantiate(obj.prefab, gameObject.transform);
                newObject.SetActive(false);
                newQueue.Enqueue(newObject);
            }

            poolDic.Add(obj.objectTag, newQueue);

        }
    }

    /// <summary>
    /// 动态添加对象池物体
    /// </summary>
    /// <param name="newObject">对象池物体对象</param>
    public void AddToPool(PoolObject newObject)
    {
        Queue<GameObject> newQueue = new Queue<GameObject>();

        for (int i = 0; i < newObject.size; i++)
        {
            GameObject newObj = Instantiate(newObject.prefab, gameObject.transform);
            newObj.SetActive(false);
            newQueue.Enqueue(newObj);
        }

        poolDic.Add(newObject.objectTag, newQueue);
    }

    /// <summary>
    /// 动态添加对象池物体
    /// </summary>
    /// <param name="objectTag">对象池物体标签名</param>
    /// <param name="prefab">物体预制体</param>
    /// <param name="size">物体初始化生成个数</param>
    public void AddToPool(string objectTag, GameObject prefab, int size)
    {
        Queue<GameObject> newQueue = new Queue<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject newObj = Instantiate(prefab, gameObject.transform);
            newObj.SetActive(false);
            newQueue.Enqueue(newObj);
        }

        poolDic.Add(objectTag, newQueue);
    }

    /// <summary>
    /// 从对象池获得对应tag名的对象
    /// </summary>
    /// <param name="tag">对象池物体标签名</param>
    /// <returns></returns>
    public GameObject GetFromPool(string tag)
    {
        //找不到对象池
        if (!poolDic.ContainsKey(tag))
        {
            Debug.LogError("Object pool doesn't contain this object's pool");
            return null;
        }

        //不够，填充队列
        if (poolDic[tag].Count == 0)
        {
            FillPool(tag);
        }

        GameObject newObj = poolDic[tag].Dequeue();

        newObj.SetActive(true);

        return newObj;
    }

    /// <summary>
    /// 从对象池获得对应tag名的对象并初始化世界坐标位置
    /// </summary>
    /// <param name="tag">对象池物体标签名</param>
    /// <param name="iniPosition">初始化物体世界坐标位置</param>
    /// <returns></returns>
    public GameObject GetFromPool(string tag, Vector3 iniPosition)
    {
        //找不到对象池
        if (!poolDic.ContainsKey(tag))
        {
            Debug.LogError("Object pool doesn't contain this object's pool");
            return null;
        }

        //不够，填充队列
        if (poolDic[tag].Count == 0)
        {
            FillPool(tag);
        }

        GameObject newObj = poolDic[tag].Dequeue();

        //设置出队物体属性
        newObj.SetActive(true);
        newObj.transform.position = iniPosition;
        return newObj;
    }

    /// <summary>
    /// 从对象池获得对应tag名的对象并初始化世界坐标位置与旋转
    /// </summary>
    /// <param name="tag">对象池物体标签名</param>
    /// <param name="iniPosition">初始化物体世界坐标位置</param>
    /// <param name="iniRotation">初始化物体世界坐标旋转</param>
    /// <returns></returns>
    public GameObject GetFromPool(string tag, Vector3 iniPosition, Quaternion iniRotation)
    {
        //找不到对象池
        if (!poolDic.ContainsKey(tag))
        {
            Debug.LogError("Object pool doesn't contain this object's pool");
            return null;
        }

        //不够，填充队列
        if (poolDic[tag].Count == 0)
        {
            FillPool(tag);
        }

        GameObject newObj = poolDic[tag].Dequeue();

        //设置出队物体属性
        newObj.SetActive(true);
        newObj.transform.position = iniPosition;
        newObj.transform.rotation = iniRotation;

        return newObj;
    }

    /// <summary>
    /// 填池子
    /// </summary>
    /// <param name="tag"></param>
    public void FillPool(string tag)
    {
        for (int i = 0; i < poolObjects.Count; i++)
        {
            if (poolObjects[i].objectTag == tag)
            {
                for (int j = 0; j < 10; j++)
                {
                    GameObject newObject = Instantiate(poolObjects[i].prefab, gameObject.transform);
                    newObject.SetActive(false);
                    poolDic[tag].Enqueue(newObject);
                }
                break;
            }
        }


    }

    /// <summary>
    /// 物体取消显示并返回对应对象池
    /// </summary>
    /// <param name="poolObj">需要回到对象池的物体</param>
    /// <param name="objectTag">对象池物体标签名</param>
    public void ReturnPool(GameObject poolObj, string objectTag)
    {
        if (!poolDic.ContainsKey(objectTag))
        {
            Debug.LogError("Object pool doesn't contain this object's pool");
            return;
        }

        //不显示
        poolObj.SetActive(false);

        //回对应池子队列
        poolDic[objectTag].Enqueue(poolObj);
    }

    /// <summary>
    /// 清除对象池生成物体
    /// </summary>
    public void ClearPool()
    {
        foreach (var item in poolDic)
        {
            item.Value.Clear();
        }
        poolDic.Clear();
        int childNum = transform.childCount;
        for (int i = 0; i < childNum; i++)
        {
            GameObject obj = transform.GetChild(0).gameObject;
            obj.SetActive(false);
#if UNITY_EDITOR
            DestroyImmediate(obj);
#else
            Destroy(obj);
#endif
        }
    }

    /// <summary>
    /// 从字典中删除对象池物体，并清除已生成对象池物体
    /// </summary>
    /// <param name="objectTag">对象池物体标签名</param>
    public void ClearPoolObjFromDic(string objectTag)
    {
        if (!poolDic.ContainsKey(objectTag))
        {
            Debug.LogError("Object pool doesn't contain this object's pool");
            return;
        }
        foreach (var item in poolDic[objectTag])
        {
            item.SetActive(false);
            Destroy(item);
        }
        poolDic.Remove(objectTag);
    }

    [System.Serializable]
    public class PoolObject
    {
        public string objectTag;
        public GameObject prefab;
        public int size;
    }

}
