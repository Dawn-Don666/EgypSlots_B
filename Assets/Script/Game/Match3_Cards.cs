using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// match3小游戏的卡片预制体
/// </summary>
public class Match3_Cards : MonoBehaviour
{
    public SkeletonGraphic anim;
    public string animName = "animation";
    public string idleName = "stay";

    private void Start()
    {
        anim.AnimationState.Complete += (t) =>
        {
            if(anim.AnimationState.GetCurrent(0).Animation.Name == animName)
            {
                anim.AnimationState.SetAnimation(0, idleName, true);
            }
        };
    }

    public void Play()
    {
        anim.Skeleton.SetToSetupPose();
        anim.AnimationState.ClearTrack(0);
        anim.AnimationState.SetAnimation(0, animName, false);
    }
}
