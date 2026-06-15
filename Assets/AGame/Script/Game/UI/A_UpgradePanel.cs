using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����ҳ��
/// </summary>
public class A_UpgradePanel : AUIWindow
{
    public Button closeBtn; //�ر�ҳ��
    public Transform listParent; //�����б��ڵ�
    public GameObject upgradeListItemPrefab;   //�����б�Ԥ����

    private Dictionary<A_UpgradeType, A_UpgradeListItem> upgradeList = new Dictionary<A_UpgradeType, A_UpgradeListItem>();

    private void Start()
    {
        closeBtn.onClick.AddListener(() => CloseUI());
    }

    /// <summary>
    /// ����ҳ��
    /// </summary>
    public override void OnCreate()
    {
        base.OnCreate(); 

        //������Ծ����
        GameObject jumpItem = Instantiate(upgradeListItemPrefab, listParent);
        jumpItem.GetComponent<A_UpgradeListItem>().UpdateItem(A_UpgradeType.Jump);
        upgradeList.Add(A_UpgradeType.Jump, jumpItem.GetComponent<A_UpgradeListItem>());

        //������ʼʱ������
        GameObject initialTimeItem = Instantiate(upgradeListItemPrefab, listParent);
        initialTimeItem.GetComponent<A_UpgradeListItem>().UpdateItem(A_UpgradeType.InitialTime);
        upgradeList.Add(A_UpgradeType.InitialTime, initialTimeItem.GetComponent<A_UpgradeListItem>());

        //����ʱ���������
        GameObject timeItemItem = Instantiate(upgradeListItemPrefab, listParent);
        timeItemItem.GetComponent<A_UpgradeListItem>().UpdateItem(A_UpgradeType.TimeItem);
        upgradeList.Add(A_UpgradeType.TimeItem, timeItemItem.GetComponent<A_UpgradeListItem>());
    }

    /// <summary>
    /// ��ҳ��
    /// </summary>
    /// <param name="callback"></param>
    public override void OnOpenAnim(Action callback = null)
    {
        base.OnOpenAnim(callback);

        //��ҳ��ˢ���б�
        upgradeList[A_UpgradeType.Jump].UpdateItem(A_UpgradeType.Jump);
        upgradeList[A_UpgradeType.InitialTime].UpdateItem(A_UpgradeType.InitialTime);
        upgradeList[A_UpgradeType.TimeItem].UpdateItem(A_UpgradeType.TimeItem);
    }
}
