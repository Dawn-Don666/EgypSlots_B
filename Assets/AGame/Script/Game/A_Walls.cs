using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 墙壁
/// </summary>
public class A_Walls : ASingletonBehaviour<A_Walls>
{
    public Transform floor; //地板
    public Transform leftWall; //左墙
    public Transform rightWall; //右墙
    public Transform background;    //背景
    public Transform recycleLine;   //回收线

    public GameObject defaultPlatformPrefab;  //默认平台预制体
    public GameObject movePlatformPrefab;   //移动平台预制体
    public GameObject conveyorPlatformPrefab;   //传送带平台预制体
    public GameObject springPlatformPrefab;   //弹簧平台预制体
    public GameObject disappearPlatformPrefab;   //自动消失平台预制体

    public float platformInterval = 1;   //平台间隔
    public int layerHasPlatform = 2;        //跳过几个平台代表一层

    private int currentPlatformNumber = 0;  //当前平台编号

    List<int> indexes = new List<int> { 0, 1, 2, 3 };   //份下标
    List<int> currIndexes = new List<int>();   //当前下标

    List<GameObject> a_Platforms = new List<GameObject>();  //存储平台列表

    private Vector3 leftWallPos;
    private Vector3 rightWallPos;
    private Vector3 backgroundPos;

    private void Start()
    {
        leftWallPos = leftWall.position;
        rightWallPos = rightWall.position;
        backgroundPos = background.position;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        //初始化地板位置
        floor.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 150, 10));

        //回收之前的平台
        for (int i = a_Platforms.Count - 1; i >= 0; i--)
        {
            A_PoolMgr.Instance.RecycleObj(a_Platforms[i]);
            a_Platforms.RemoveAt(i);
        }
        a_Platforms.Clear();

        //重置墙壁和背景位置
        leftWall.position = leftWallPos;
        rightWall.position = rightWallPos;
        background.position = backgroundPos;

        //创建平台
        currentPlatformNumber = 0;
        CreatPlatform(30);
    }

    /// <summary>
    /// 创建平台
    /// </summary>
    /// <param name="platformNumber"></param>
    void CreatPlatform(int platformNumber)
    {
        for (int i = 0; i < platformNumber; i++)
        {
            currentPlatformNumber++;    //增加一层
 
            int sum = A_Config.defaultPlatformWight + A_Config.movePlatformWight + A_Config.conveyorPlatformWight + A_Config.springPlatformWight + A_Config.disappearPlatformWight;
            int rand = Random.Range(0, sum);  //随机选择平台类型

            //创建平台
            GameObject platform;
            if (rand < A_Config.defaultPlatformWight)   //创建默认平台
            {
                platform = A_PoolMgr.Instance.GetObj(defaultPlatformPrefab);
            }
            else if(rand < A_Config.defaultPlatformWight + A_Config.movePlatformWight)  //创建移动平台
            {
                platform = A_PoolMgr.Instance.GetObj(movePlatformPrefab);
            }
            else if(rand < A_Config.defaultPlatformWight + A_Config.movePlatformWight + A_Config.conveyorPlatformWight)     //创建传送平台
            {
                platform = A_PoolMgr.Instance.GetObj(conveyorPlatformPrefab);
            }
            else if(rand < A_Config.defaultPlatformWight + A_Config.movePlatformWight + A_Config.conveyorPlatformWight + A_Config.springPlatformWight)   //创建弹簧平台
            {
                platform = A_PoolMgr.Instance.GetObj(springPlatformPrefab);
            }
            else    //创建自动消失平台
            {
                platform = A_PoolMgr.Instance.GetObj(disappearPlatformPrefab);
            }

            //摆放位置
            platform.transform.SetParent(transform);
            platform.transform.SetAsLastSibling();
            float left = leftWall.position.x + leftWall.GetComponent<SpriteRenderer>().bounds.size.x / 2 + platform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
            float right = rightWall.position.x - rightWall.GetComponent<SpriteRenderer>().bounds.size.x / 2 - platform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
            float high = floor.position.y + platformInterval * currentPlatformNumber;
            if (currIndexes.Count == 0) currIndexes.AddRange(indexes);  //如果待选的下标用完了，则重新填满待选下标
            int index = Random.Range(0, currIndexes.Count); //选出来的下标的下标
            int currIndex = currIndexes[index];     //选出来的下标
            
            float randX = RandomR(left, right, 4, currIndex);
            currIndexes.RemoveAt(index);   //移除选出来的下标

            platform.transform.position = new Vector3(randX, high, 0);
            platform.GetComponent<A_Platform>().Init(currentPlatformNumber);
            a_Platforms.Add(platform);  //存储起来
        }
    }

    /// <summary>
    /// 更新平台
    /// </summary>
    public void UpdatePlatforms()
    {
        int needRecycle = 0;
        //低于回收线的平台自动回收
        for(int i = a_Platforms.Count - 1; i >= 0; i--)
        {
            if(a_Platforms[i].transform.position.y < recycleLine.position.y)
            {
                needRecycle++;
                A_PoolMgr.Instance.RecycleObj(a_Platforms[i]);
                a_Platforms.RemoveAt(i);
            }  
        }

        //同时创建新的平台
        CreatPlatform(needRecycle);
    }

    private void Update()
    {
        UpdatePlatforms();
    }

    /// <summary>
    /// 防止纯随机造成的扎堆和空挡问题
    /// </summary>
    /// <param name="min">最小值</param>
    /// <param name="max">最大值</param>
    /// <param name="portion">分成多少份</param>
    /// <param name="index">份的index</param>
    float RandomR(float min, float max, int portion, int index, float offset = 0.25f)
    {
        float everyPortion = (max - min) / portion;
        float leftOffset = offset;
        float rightOffset = offset;
        if (index == 0) leftOffset = 0;
        else if (index == portion - 1)  rightOffset = 0;
        return Random.Range(min + everyPortion * index + leftOffset, min + everyPortion * (index + 1) - rightOffset);
    }
}

/// <summary>
/// 平台种类
/// </summary>
public enum A_E_PlatformType
{
    DefaultPlatform,    //默认平台
    MovePlatform,       //可移动平台
    ConveyorPlatform,   //传送带平台
    SpringPlatform      //弹簧平台
}
