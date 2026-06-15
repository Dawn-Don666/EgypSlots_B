using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// ×ª³¡¶¯»­
/// </summary>
public class Transition : MonoBehaviour
{
    public UnityAction action;

    public void Play()
    {
        gameObject.SetActive(true);
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
        GetComponent<Image>().DOFade(1, 0.8f);
        StartCoroutine(Hide());
    }
    IEnumerator Hide()
    {
        yield return new WaitForSeconds(0.8f);
        GetComponent<Image>().DOFade(0, 0.8f);
    }
}
