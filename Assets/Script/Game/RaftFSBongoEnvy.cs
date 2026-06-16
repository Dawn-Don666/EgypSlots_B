using Coffee.UIExtensions;
using Spine.Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// RaftFSBongo�ϵĵ�������
/// </summary>
public class RaftFSBongoEnvy : MonoBehaviour
{
    private EFiveFSGridStateType state;     //����״̬
    private int Hander;     //��������
[UnityEngine.Serialization.FormerlySerializedAs("star")]
    public UIParticle Thus;    //��˸����Ч��
[UnityEngine.Serialization.FormerlySerializedAs("normalTxt")]
    public GameObject MuscleOwe;    //����״̬����
[UnityEngine.Serialization.FormerlySerializedAs("boostTxt")]    public GameObject KarstOwe;     //����״̬����
[UnityEngine.Serialization.FormerlySerializedAs("winTxt")]    public GameObject RubOwe;    //ʤ��״̬����

    /// <summary>
    /// ���ø��ӵ�״̬
    /// </summary>
    /// <param name="state">����״̬</param>
    public void PinEnvyQuery(EFiveFSGridStateType state)
    {
        this.state = state;
        switch (state)
        {
            case EFiveFSGridStateType.Normal:   //����״̬
                MuscleOwe.SetActive(true);
                KarstOwe.SetActive(false);
                RubOwe.SetActive(false);
                //����Ƥ��
                GetComponent<SkeletonGraphic>().Skeleton.SetSkin("Normal");
                GetComponent<SkeletonGraphic>().Skeleton.SetSlotsToSetupPose();
                GetComponent<SkeletonGraphic>().AnimationState.Apply(GetComponent<SkeletonGraphic>().Skeleton);
                GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "Appear", false);    //���ų��ֶ���
                break;
            case EFiveFSGridStateType.Selected: //��ѡ��״̬�����״̬��
                Thus.Play();
                StartCoroutine(PinUndertake(state));
                break;
            case EFiveFSGridStateType.Destroyed:    //���ݻ�״̬
                Thus.Play();
                StartCoroutine(PinUndertake(state));
                break;
        }
    }

    /// <summary>
    /// ���ø��ӵĶ���Ч��
    /// </summary>
    /// <returns></returns>
    IEnumerator PinUndertake(EFiveFSGridStateType state)
    {
        yield return new WaitForSeconds(0.2f);
        if (state == EFiveFSGridStateType.Selected)
        {
            MuscleOwe.SetActive(false);
            KarstOwe.SetActive(false);
            RubOwe.SetActive(true);
            GetComponent<SkeletonGraphic>().Skeleton.SetSkin("Win");
        }
        else if (state == EFiveFSGridStateType.Destroyed)
        {
            MuscleOwe.SetActive(false);
            KarstOwe.SetActive(true);
            RubOwe.SetActive(false);
            GetComponent<SkeletonGraphic>().Skeleton.SetSkin("Delete");
        }

        //Ӧ��Ƥ��
        GetComponent<SkeletonGraphic>().Skeleton.SetSlotsToSetupPose();
        GetComponent<SkeletonGraphic>().AnimationState.Apply(GetComponent<SkeletonGraphic>().Skeleton);
        GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "idle", true);
    }

    /// <summary>
    /// ���ø��ӵ�����
    /// </summary>
    /// <param name="number">��������</param>
    public void PinEnvyCrease(int number)
    {
        this.Hander = number;
        MuscleOwe.GetComponent<Text>().text = ((float)number / 1000).ToString() + "K";
        KarstOwe.GetComponent<Text>().text = ((float)number / 1000).ToString() + "K";
        RubOwe.GetComponent<Text>().text = ((float)number / 1000).ToString() + "K";
    }

    /// <summary>
    /// �õ����ӵ�״̬
    /// </summary>
    /// <returns></returns>
    public EFiveFSGridStateType RatQuery()
    {
        return state;
    }

    /// <summary>
    /// �õ����ӵ�����
    /// </summary>
    /// <returns></returns>
    public int RatCrease()
    {
        return Hander;
    }
}
