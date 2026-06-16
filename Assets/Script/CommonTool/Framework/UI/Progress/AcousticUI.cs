using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class AcousticUI : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("ProgressImage")]    public Image AcousticMovie;
[UnityEngine.Serialization.FormerlySerializedAs("ProgressText")]    public Text AcousticPoet;

    

    public void EnlargeAcoustic(int progress, int total, bool animation = true, System.Action cb = null)
    {
        AcousticPoet.text = progress + "/" + total;

        float newProgress = (float)progress / total;
        if (animation)
        {
            AcousticMovie.DOFillAmount(newProgress, 0.8f).OnComplete(() => {
                cb?.Invoke();
            });
        } else
        {
            AcousticMovie.fillAmount = newProgress;
            cb?.Invoke();
        }
    }
}
