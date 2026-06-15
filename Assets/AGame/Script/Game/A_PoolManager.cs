using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池模块
/// </summary>
public class A_PoolMgr : ASingleton<A_PoolMgr>
{
    public Dictionary<string, Queue<GameObject>> poolDic = new Dictionary<string, Queue<GameObject>>();

    /// <summary>
    /// 得到池子中的对象
    /// </summary>
    /// <param name="prefab">生成对象的预制体</param>
    /// <returns>取出的对象</returns>
    public GameObject GetObj(GameObject prefab)
    {
        GameObject obj = null;
        //判断有这个子模块并且子模块中还得有对象
        if (poolDic.ContainsKey(prefab.name) && poolDic[prefab.name].Count > 0)
        {
            obj = poolDic[prefab.name].Dequeue();
        }
        else
        {
            obj = GameObject.Instantiate(prefab);
            //将对象的名字设置为和池子子模块的名字一样，这样就可以在PushObj中用对象名字存储
            obj.name = prefab.name;
        }
        //物体激活，让其显示
        obj.SetActive(true);
        obj.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);    //如果这个对象的脚本中有OnSpawn方法就执行
        return obj;
    }

    /// <summary>
    /// 回收对象
    /// </summary>
    /// <param name="obj">要回收的对象</param>
    public void RecycleObj(GameObject obj)
    {
        //把物体失活，让其隐藏
        obj.SendMessage("OnDespawn", SendMessageOptions.DontRequireReceiver);    //如果这个对象的脚本中有OnDespawn方法就执行
        obj.SetActive(false);
        //如果没有这个子模块就创建子模块再存
        if (!poolDic.ContainsKey(obj.name))
        {
            poolDic.Add(obj.name, new Queue<GameObject>());
        }
        poolDic[obj.name].Enqueue(obj);
    }

    /// <summary>
    /// 清空缓存池
    /// </summary>
    public void Clear()
    {
        poolDic.Clear();
    }
}
