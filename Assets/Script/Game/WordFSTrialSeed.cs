using Coffee.UIExtensions;
using Spine.Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// WordFSTrial�ϵĵ�������
/// </summary>
public class WordFSTrialSeed : MonoBehaviour
{
    private EFiveFSGridStateType state;     //����״̬
    private int Warmth;     //��������
[UnityEngine.Serialization.FormerlySerializedAs("star")]
    public UIParticle Coal;    //��˸����Ч��
[UnityEngine.Serialization.FormerlySerializedAs("normalTxt")]
    public GameObject AroundUse;    //����״̬����
[UnityEngine.Serialization.FormerlySerializedAs("boostTxt")]    public GameObject RulerUse;     //����״̬����
[UnityEngine.Serialization.FormerlySerializedAs("winTxt")]    public GameObject BogUse;    //ʤ��״̬����

    /// <summary>
    /// ���ø��ӵ�״̬
    /// </summary>
    /// <param name="state">����״̬</param>
    public void PigSeedWaste(EFiveFSGridStateType state)
    {
        this.state = state;
        switch (state)
        {
            case EFiveFSGridStateType.Normal:   //����״̬
                AroundUse.SetActive(true);
                RulerUse.SetActive(false);
                BogUse.SetActive(false);
                //����Ƥ��
                GetComponent<SkeletonGraphic>().Skeleton.SetSkin("Normal");
                GetComponent<SkeletonGraphic>().Skeleton.SetSlotsToSetupPose();
                GetComponent<SkeletonGraphic>().AnimationState.Apply(GetComponent<SkeletonGraphic>().Skeleton);
                GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "Appear", false);    //���ų��ֶ���
                break;
            case EFiveFSGridStateType.Selected: //��ѡ��״̬�����״̬��
                Coal.Play();
                StartCoroutine(PigComponent(state));
                break;
            case EFiveFSGridStateType.Destroyed:    //���ݻ�״̬
                Coal.Play();
                StartCoroutine(PigComponent(state));
                break;
        }
    }

    /// <summary>
    /// ���ø��ӵĶ���Ч��
    /// </summary>
    /// <returns></returns>
    IEnumerator PigComponent(EFiveFSGridStateType state)
    {
        yield return new WaitForSeconds(0.2f);
        if (state == EFiveFSGridStateType.Selected)
        {
            AroundUse.SetActive(false);
            RulerUse.SetActive(false);
            BogUse.SetActive(true);
            GetComponent<SkeletonGraphic>().Skeleton.SetSkin("Win");
        }
        else if (state == EFiveFSGridStateType.Destroyed)
        {
            AroundUse.SetActive(false);
            RulerUse.SetActive(true);
            BogUse.SetActive(false);
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
    public void PigSeedJewett(int number)
    {
        this.Warmth = number;
        AroundUse.GetComponent<Text>().text = ((float)number / 1000).ToString() + "K";
        RulerUse.GetComponent<Text>().text = ((float)number / 1000).ToString() + "K";
        BogUse.GetComponent<Text>().text = ((float)number / 1000).ToString() + "K";
    }

    /// <summary>
    /// �õ����ӵ�״̬
    /// </summary>
    /// <returns></returns>
    public EFiveFSGridStateType TieWaste()
    {
        return state;
    }

    /// <summary>
    /// �õ����ӵ�����
    /// </summary>
    /// <returns></returns>
    public int TieJewett()
    {
        return Warmth;
    }
}
