using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tab按钮样式脚本
/// </summary>

public class OweBustNeutrality : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("Icon")]    public Image Mute;
[UnityEngine.Serialization.FormerlySerializedAs("Title")]    public Text Width;

    

    public void PinRemoteUI(bool active, OweNeutrality controller, TabItem tabItem)
    {
        if (Width != null && controller.RemoteRoyal != null)
        {
            Width.color = active ? controller.RemoteRoyal : controller.DiscipleRoyal;
        }
        if (gameObject.GetComponent<Image>() != null && controller.RemoteBG != null)
        {
            gameObject.GetComponent<Image>().sprite = active ? controller.RemoteBG : controller.DiscipleBG;
        }
        if (Mute != null && tabItem.RemoteMute != null)
        {
            Mute.sprite = active ? tabItem.RemoteMute : tabItem.DiscipleMute;
        }
    }
}
