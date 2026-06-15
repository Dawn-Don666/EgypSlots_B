using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tab按钮样式脚本
/// </summary>

public class GarFineCretaceous : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("Icon")]    public Image City;
[UnityEngine.Serialization.FormerlySerializedAs("Title")]    public Text Every;

    

    public void PigNegateUI(bool active, GarCretaceous controller, TabItem tabItem)
    {
        if (Every != null && controller.NegateSmoky != null)
        {
            Every.color = active ? controller.NegateSmoky : controller.SavannahSmoky;
        }
        if (gameObject.GetComponent<Image>() != null && controller.NegateBG != null)
        {
            gameObject.GetComponent<Image>().sprite = active ? controller.NegateBG : controller.SavannahBG;
        }
        if (City != null && tabItem.NegateCity != null)
        {
            City.sprite = active ? tabItem.NegateCity : tabItem.SavannahCity;
        }
    }
}
