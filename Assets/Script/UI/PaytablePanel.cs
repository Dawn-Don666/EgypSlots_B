using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 从设置里点开的一张pays图片
/// </summary>
public class PaytablePanel : BaseUIForms
{
    public Button backBtn;  //返回按钮
    public RectTransform content;  //内容区域
    void Start()
    {
        backBtn.onClick.AddListener(BackBtnClick);
    }

    public void Init()
    {
        content.anchoredPosition = Vector2.zero;
    }

    /// <summary>
    /// 返回
    /// </summary>
    void BackBtnClick()
    {
        CloseUIForm(nameof(PaytablePanel));
    }
}
