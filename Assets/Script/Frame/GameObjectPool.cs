using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池
/// </summary>
public class GameObjectPool : Singleton<GameObjectPool>
{
    public Dictionary<string, Queue<GameObject>> poolDic = new Dictionary<string, Queue<GameObject>>();

    /// <summary>
    /// 对象池取出对象
    /// </summary>
    /// <param name="name">对象名</param>
    /// <param name="gameObj">对象预制体</param>
    /// <returns></returns>
    public GameObject GetObj(string name, GameObject gameObj)
    {
        GameObject obj = null;
        //判断有这个子模块并且子模块中还得有对象
        if (poolDic.ContainsKey(name) && poolDic[name].Count > 0)
        {
            //拿到第0个对象
            obj = poolDic[name].Dequeue();
        }
        else
        {
            //如果池子里没有对象就创建对象
            obj = GameObject.Instantiate(gameObj);
            //将对象的名字设置为和池子子模块的名字一样，这样就可以在PushObj中用对象名字存储
            obj.name = name;
        }
        //物体激活，让其显示
        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// 将对象回收回池子中
    /// </summary>
    /// <param name="name"></param>
    /// <param name="obj"></param>
    public void PushObj(GameObject obj)
    {
        //把物体失活，让其隐藏
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
