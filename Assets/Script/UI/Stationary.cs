using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// ת������
/// </summary>
public class Stationary : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("action")]    public UnityAction Acidic;

    public void Boot()
    {
        gameObject.SetActive(true);
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
        GetComponent<Image>().DOFade(1, 0.8f);
        StartCoroutine(Berg());
    }
    IEnumerator Berg()
    {
        yield return new WaitForSeconds(0.8f);
        GetComponent<Image>().DOFade(0, 0.8f);
    }
}
