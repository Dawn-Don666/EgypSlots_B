using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 消息传递的参数
/// </summary>
public class CollectLieu
{
    /*
     *  1.创建独立的消息传递数据结构，而不使用object，是为了避免数据传递时的类型强转
     *  2.制作过程中遇到实际需要传递的数据类型，在这里定义即可
     *  3.实际项目中需要传递参数的类型其实并没有很多种，这种方式基本可以满足需求
     */
    public bool UnderDeer;
    public bool UnderDeer2;
    public int UnderFen;
    public int UnderFen2;
    public int UnderFen3;
    public float UnderSolve;
    public float UnderSolve2;
    public double UnderRancho;
    public double UnderRancho2;
    public string UnderDemand;
    public string UnderDemand2;
    public GameObject UnderSinkBedpan;
    public GameObject UnderSinkBedpan2;
    public GameObject UnderSinkBedpan3;
    public GameObject UnderSinkBedpan4;
    public Transform UnderSingleton;
    public List<string> UnderDemandFond;
    public List<Vector2> UnderSpy2Fond;
    public List<int> UnderFenFond;
    public System.Action SinuousLintDuck;
    public Vector2 Him2_1;
    public Vector2 Him2_2;
    public CollectLieu()
    {
    }
    public CollectLieu(Vector2 v2_1)
    {
        Him2_1 = v2_1;
    }
    public CollectLieu(Vector2 v2_1, Vector2 v2_2)
    {
        Him2_1 = v2_1;
        Him2_2 = v2_2;
    }
    /// <summary>
    /// 创建一个带bool类型的数据
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public CollectLieu(bool value)
    {
        UnderDeer = value;
    }
    public CollectLieu(bool value, bool value2)
    {
        UnderDeer = value;
        UnderDeer2 = value2;
    }
    /// <summary>
    /// 创建一个带int类型的数据
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public CollectLieu(int value)
    {
        UnderFen = value;
    }
    public CollectLieu(int value, int value2)
    {
        UnderFen = value;
        UnderFen2 = value2;
    }
    public CollectLieu(int value, int value2, int value3)
    {
        UnderFen = value;
        UnderFen2 = value2;
        UnderFen3 = value3;
    }
    public CollectLieu(List<int> value,List<Vector2> value2)
    {
        UnderFenFond = value;
        UnderSpy2Fond = value2;
    }
    /// <summary>
    /// 创建一个带float类型的数据
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public CollectLieu(float value)
    {
        UnderSolve = value;
    }
    public CollectLieu(float value,float value2)
    {
        UnderSolve = value;
        UnderSolve = value2;
    }
    /// <summary>
    /// 创建一个带double类型的数据
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public CollectLieu(double value)
    {
        UnderRancho = value;
    }
    public CollectLieu(double value, double value2)
    {
        UnderRancho = value;
        UnderRancho = value2;
    }
    /// <summary>
    /// 创建一个带string类型的数据
    /// </summary>
    /// <param name="value"></param>
    public CollectLieu(string value)
    {
        UnderDemand = value;
    }
    /// <summary>
    /// 创建两个带string类型的数据
    /// </summary>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    public CollectLieu(string value1,string value2)
    {
        UnderDemand = value1;
        UnderDemand2 = value2;
    }
    public CollectLieu(GameObject value1)
    {
        UnderSinkBedpan = value1;
    }

    public CollectLieu(Transform transform)
    {
        UnderSingleton = transform;
    }
}

