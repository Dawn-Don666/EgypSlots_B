using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����
/// </summary>
public class GameObjectPool : Youngster<GameObjectPool>
{
    public Dictionary<string, Queue<GameObject>> poolDic = new Dictionary<string, Queue<GameObject>>();

    /// <summary>
    /// �����ȡ������
    /// </summary>
    /// <param name="name">������</param>
    /// <param name="gameObj">����Ԥ����</param>
    /// <returns></returns>
    public GameObject GetObj(string name, GameObject gameObj)
    {
        GameObject obj = null;
        //�ж��������ģ�鲢����ģ���л����ж���
        if (poolDic.ContainsKey(name) && poolDic[name].Count > 0)
        {
            //�õ���0������
            obj = poolDic[name].Dequeue();
        }
        else
        {
            //���������û�ж���ʹ�������
            obj = GameObject.Instantiate(gameObj);
            //���������������Ϊ�ͳ�����ģ�������һ���������Ϳ�����PushObj���ö������ִ洢
            obj.name = name;
        }
        //���弤�������ʾ
        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// ��������ջس�����
    /// </summary>
    /// <param name="name"></param>
    /// <param name="obj"></param>
    public void PushObj(GameObject obj)
    {
        //������ʧ���������
        obj.SetActive(false);
        //���û�������ģ��ʹ�����ģ���ٴ�
        if (!poolDic.ContainsKey(obj.name))
        {
            poolDic.Add(obj.name, new Queue<GameObject>());
        }
        poolDic[obj.name].Enqueue(obj);
    }

    /// <summary>
    /// ��ջ����
    /// </summary>
    public void Clear()
    {
        poolDic.Clear();
    }
}
