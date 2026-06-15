using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class PreserveUI : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("ProgressImage")]    public Image PreserveWhite;
[UnityEngine.Serialization.FormerlySerializedAs("ProgressText")]    public Text PreserveCrew;

    

    public void TopicalPreserve(int progress, int total, bool animation = true, System.Action cb = null)
    {
        PreserveCrew.text = progress + "/" + total;

        float newProgress = (float)progress / total;
        if (animation)
        {
            PreserveWhite.DOFillAmount(newProgress, 0.8f).OnComplete(() => {
                cb?.Invoke();
            });
        } else
        {
            PreserveWhite.fillAmount = newProgress;
            cb?.Invoke();
        }
    }
}
