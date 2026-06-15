using System.Collections;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 提示面板。
/// </summary>
/// <param name="tips">提示内容。</param>
public class ATipsPanel : AUIWindow
{
    public Text m_TipsText;
    
    public override void OnRefresh()
    {
        base.OnRefresh();
        m_TipsText.text = (string)UserData;
        
        StartCoroutine(ClosePanel(1.5f));
    }
    
    private IEnumerator ClosePanel(float time)
    {
        yield return new WaitForSeconds(time);
        CloseUI<ATipsPanel>();
    }
}