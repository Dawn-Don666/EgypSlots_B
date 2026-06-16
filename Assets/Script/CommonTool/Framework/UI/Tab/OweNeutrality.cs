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
    public string TabForm;
    [SerializeField]
    private GameObject Quick= null;
    public GameObject Coast{ get { return Quick; } }

    [SerializeField]
    private Button tabShroud= null;
    public Button OweShroud{ get { return tabShroud; } }

    public Sprite RemoteMute;
    public Sprite DiscipleMute;
}

public class OweNeutrality : MonoBehaviour
{
    [SerializeField]
[UnityEngine.Serialization.FormerlySerializedAs("items")]    public List<TabItem> Audio= null;
[UnityEngine.Serialization.FormerlySerializedAs("Content")]
    public GameObject Chemist;
[UnityEngine.Serialization.FormerlySerializedAs("ActiveAnimationObj")]    public GameObject RemoteUndertakePet;
[UnityEngine.Serialization.FormerlySerializedAs("ActiveBG")]    public Sprite RemoteBG;
[UnityEngine.Serialization.FormerlySerializedAs("InactiveBG")]    public Sprite DiscipleBG;
[UnityEngine.Serialization.FormerlySerializedAs("ActiveColor")]    public Color RemoteRoyal;
[UnityEngine.Serialization.FormerlySerializedAs("InactiveColor")]    public Color DiscipleRoyal;
    [Header("初始选中Tab名称")]
[UnityEngine.Serialization.FormerlySerializedAs("ActiveTab")]    public GameObject RemoteOwe;

    private string NarrowOweForm;

    private Dictionary<string, GameObject> TieFodder;

    private Action<string, GameObject> CiteDoctrine;    // 打开tab回调

    // Start is called before the first frame update
    void Start()
    {
        TieFodder = new Dictionary<string, GameObject>();

        // Tab按钮绑定点击事件
        foreach (TabItem tabItem in Audio)
        {
            tabItem.OweShroud.onClick.AddListener(() =>
            {
                PaceOwe(tabItem.TabForm);
            });
        }

        if (RemoteOwe != null)
        {
            foreach(TabItem tab in Audio)
            {
                if (tab.OweShroud.gameObject == RemoteOwe)
                {
                    NarrowOweForm = tab.TabForm;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(NarrowOweForm))
            {
                PaceOwe(NarrowOweForm);
            }
        }
    }

    /// <summary>
    /// 打开tab页面
    /// </summary>
    /// <param name="_tabName"></param>
    public GameObject PaceOwe(string _tabName)
    {
        if (!string.IsNullOrEmpty(NarrowOweForm) && TieFodder.ContainsKey(NarrowOweForm))
        {
            if (TieFodder[NarrowOweForm].GetComponent<AeroUIOnset>() != null)
            {
                TieFodder[NarrowOweForm].GetComponent<AeroUIOnset>().Hidding();
            }
            else
            {
                TieFodder[NarrowOweForm].SetActive(false);
            }
        }

        GameObject activeTabItem = null;
        foreach (TabItem tabItem in Audio)
        {
            tabItem.OweShroud.GetComponent<OweBustNeutrality>().PinRemoteUI(tabItem.TabForm.Equals(_tabName), this, tabItem);
            if (tabItem.TabForm.Equals(_tabName))
            {
                activeTabItem = tabItem.OweShroud.gameObject;
                if (!TieFodder.ContainsKey(_tabName) && tabItem.Coast != null)
                {
                    GameObject tabItemPanel = Chemist.transform.Find(tabItem.Coast.name) == null ? Instantiate(tabItem.Coast, Chemist.transform) : tabItem.Coast;
                    TieFodder.Add(_tabName, tabItemPanel);
                }
            }
        }
        if (TieFodder.ContainsKey(_tabName))
        {
            if (TieFodder[_tabName].GetComponent<AeroUIOnset>() != null)
            {
                TieFodder[_tabName].GetComponent<AeroUIOnset>().Display(null);
            }
            else
            {
                TieFodder[_tabName]?.SetActive(true);
            }
        }

        NarrowOweForm = _tabName;

        StartCoroutine(RemoteOrUndertake(activeTabItem));

        CiteDoctrine?.Invoke(_tabName, TieFodder.ContainsKey(_tabName) ? TieFodder[_tabName] : null);

        return TieFodder.ContainsKey(_tabName) ? TieFodder[_tabName] : null;
    }

    // tab背景动画
    private IEnumerator RemoteOrUndertake(GameObject activeTabItem)
    {
        yield return new WaitForEndOfFrame();
        if (activeTabItem != null && RemoteUndertakePet != null)
        {
            RemoteUndertakePet.transform.SetParent(activeTabItem.transform);
            RemoteUndertakePet.transform.SetSiblingIndex(0);
            RemoteUndertakePet.GetComponent<RectTransform>().DOMoveX(activeTabItem.GetComponent<RectTransform>().position.x, 0.3f).SetEase(Ease.OutBack);
        }
    }

    /// <summary>
    /// 注册打开tab回调事件
    /// </summary>
    /// <param name="_callback"></param>
    public void CetaceanDoctrine(Action<string, GameObject> _callback)
    {
        CiteDoctrine = _callback;
    }
}
