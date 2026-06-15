using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// match3С��Ϸ�Ŀ�ƬԤ����
/// </summary>
public class Chile3_Aside : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("anim")]    public SkeletonGraphic Soft;
[UnityEngine.Serialization.FormerlySerializedAs("animName")]    public string SoftLady= "animation";
[UnityEngine.Serialization.FormerlySerializedAs("idleName")]    public string ObeyLady= "stay";

    private void Start()
    {
        Soft.AnimationState.Complete += (t) =>
        {
            if(Soft.AnimationState.GetCurrent(0).Animation.Name == SoftLady)
            {
                Soft.AnimationState.SetAnimation(0, ObeyLady, true);
            }
        };
    }

    public void Beer()
    {
        Soft.Skeleton.SetToSetupPose();
        Soft.AnimationState.ClearTrack(0);
        Soft.AnimationState.SetAnimation(0, SoftLady, false);
    }
}
