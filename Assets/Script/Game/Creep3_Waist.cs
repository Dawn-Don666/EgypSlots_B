using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// match3С��Ϸ�Ŀ�ƬԤ����
/// </summary>
public class Creep3_Waist : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("anim")]    public SkeletonGraphic Peck;
[UnityEngine.Serialization.FormerlySerializedAs("animName")]    public string PeckForm= "animation";
[UnityEngine.Serialization.FormerlySerializedAs("idleName")]    public string BareForm= "stay";

    private void Start()
    {
        Peck.AnimationState.Complete += (t) =>
        {
            if(Peck.AnimationState.GetCurrent(0).Animation.Name == PeckForm)
            {
                Peck.AnimationState.SetAnimation(0, BareForm, true);
            }
        };
    }

    public void Boot()
    {
        Peck.Skeleton.SetToSetupPose();
        Peck.AnimationState.ClearTrack(0);
        Peck.AnimationState.SetAnimation(0, PeckForm, false);
    }
}
