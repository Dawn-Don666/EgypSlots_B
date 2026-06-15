using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 导航插件
/// </summary>

[Serializable]
public class TabItem
{
    public string GarLady;
    [SerializeField]
    private GameObject Story= null;
    public GameObject Trick{ get { return Story; } }

    [SerializeField]
    private Button IonDampen= null;
    public Button GarDampen{ get { return IonDampen; } }

    public Sprite NegateCity;
    public Sprite SavannahCity;
}

public class GarCretaceous : MonoBehaviour
{
    [SerializeField]
[UnityEngine.Serialization.FormerlySerializedAs("items")]    public List<TabItem> Storm= null;
[UnityEngine.Serialization.FormerlySerializedAs("Content")]
    public GameObject Treason;
[UnityEngine.Serialization.FormerlySerializedAs("ActiveAnimationObj")]    public GameObject NegateComponentFox;
[UnityEngine.Serialization.FormerlySerializedAs("ActiveBG")]    public Sprite NegateBG;
[UnityEngine.Serialization.FormerlySerializedAs("InactiveBG")]    public Sprite SavannahBG;
[UnityEngine.Serialization.FormerlySerializedAs("ActiveColor")]    public Color NegateSmoky;
[UnityEngine.Serialization.FormerlySerializedAs("InactiveColor")]    public Color SavannahSmoky;
    [Header("初始选中Tab名称")]
[UnityEngine.Serialization.FormerlySerializedAs("ActiveTab")]    public GameObject NegateGar;

    private string InjuryGarLady;

    private Dictionary<string, GameObject> IonFiring;

    private Action<string, GameObject> CardSinkhole;    // 打开tab回调

    // Start is called before the first frame update
    void Start()
    {
        IonFiring = new Dictionary<string, GameObject>();

        // Tab按钮绑定点击事件
        foreach (TabItem tabItem in Storm)
        {
            tabItem.GarDampen.onClick.AddListener(() =>
            {
                SpanGar(tabItem.GarLady);
            });
        }

        if (NegateGar != null)
        {
            foreach(TabItem tab in Storm)
            {
                if (tab.GarDampen.gameObject == NegateGar)
                {
                    InjuryGarLady = tab.GarLady;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(InjuryGarLady))
            {
                SpanGar(InjuryGarLady);
            }
        }
    }

    /// <summary>
    /// 打开tab页面
    /// </summary>
    /// <param name="_tabName"></param>
    public GameObject SpanGar(string _tabName)
    {
        if (!string.IsNullOrEmpty(InjuryGarLady) && IonFiring.ContainsKey(InjuryGarLady))
        {
            if (IonFiring[InjuryGarLady].GetComponent<FilmUIFetus>() != null)
            {
                IonFiring[InjuryGarLady].GetComponent<FilmUIFetus>().Hidding();
            }
            else
            {
                IonFiring[InjuryGarLady].SetActive(false);
            }
        }

        GameObject activeTabItem = null;
        foreach (TabItem tabItem in Storm)
        {
            tabItem.GarDampen.GetComponent<GarFineCretaceous>().PigNegateUI(tabItem.GarLady.Equals(_tabName), this, tabItem);
            if (tabItem.GarLady.Equals(_tabName))
            {
                activeTabItem = tabItem.GarDampen.gameObject;
                if (!IonFiring.ContainsKey(_tabName) && tabItem.Trick != null)
                {
                    GameObject tabItemPanel = Treason.transform.Find(tabItem.Trick.name) == null ? Instantiate(tabItem.Trick, Treason.transform) : tabItem.Trick;
                    IonFiring.Add(_tabName, tabItemPanel);
                }
            }
        }
        if (IonFiring.ContainsKey(_tabName))
        {
            if (IonFiring[_tabName].GetComponent<FilmUIFetus>() != null)
            {
                IonFiring[_tabName].GetComponent<FilmUIFetus>().Display(null);
            }
            else
            {
                IonFiring[_tabName]?.SetActive(true);
            }
        }

        InjuryGarLady = _tabName;

        StartCoroutine(NegateOnComponent(activeTabItem));

        CardSinkhole?.Invoke(_tabName, IonFiring.ContainsKey(_tabName) ? IonFiring[_tabName] : null);

        return IonFiring.ContainsKey(_tabName) ? IonFiring[_tabName] : null;
    }

    // tab背景动画
    private IEnumerator NegateOnComponent(GameObject activeTabItem)
    {
        yield return new WaitForEndOfFrame();
        if (activeTabItem != null && NegateComponentFox != null)
        {
            NegateComponentFox.transform.SetParent(activeTabItem.transform);
            NegateComponentFox.transform.SetSiblingIndex(0);
            NegateComponentFox.GetComponent<RectTransform>().DOMoveX(activeTabItem.GetComponent<RectTransform>().position.x, 0.3f).SetEase(Ease.OutBack);
        }
    }

    /// <summary>
    /// 注册打开tab回调事件
    /// </summary>
    /// <param name="_callback"></param>
    public void AdvocateSinkhole(Action<string, GameObject> _callback)
    {
        CardSinkhole = _callback;
    }
}
