using System.Collections.Generic;

/// <summary>
/// ӳ��
/// </summary>
public class Lily
{
    /// <summary>
    /// spine������ӳ��
    /// </summary>
    public static Dictionary<string, string> WeighSackFormSad= new Dictionary<string, string>() 
    {
        //Slots����
        { "WindDefault","animation"},   //���ܱ�־Ĭ�ϴ�������
        { "CleopatraDefault","animation"},   //�߼�ͼ��1Ĭ�ϴ�������
        { "AnkhDefault","animation"},   //�߼�ͼ��2Ĭ�ϴ�������
        { "HonusDefault","animation"},   //�߼�ͼ��3Ĭ�ϴ�������
        { "JarDefault","animation"},   //�м�ͼ��1Ĭ�ϴ�������
        { "RingDefault","animation"},   //�м�ͼ��2Ĭ�ϴ�������
        { "TenDefault","10"},   //�ͼ�ͼ��1Ĭ�ϴ�������
        { "JDefault","j"},   //�ͼ�ͼ��2Ĭ�ϴ�������
        { "QDefault","q"},   //�ͼ�ͼ��3Ĭ�ϴ�������
        { "KDefault","k"},   //�ͼ�ͼ��4Ĭ�ϴ�������
        { "ADefault","a"},   //�ͼ�ͼ��5Ĭ�ϴ�������
        { "ScratchDefault","animation"},   //�ιο�Ĭ�ϴ�������
        { "ScatterDefault","animation"},   //ScatterĬ�ϴ�������
        { "LuckyWheelDefault","animation"},   //����ת��Ĭ�ϴ�������
        { "MagicBugTrigger","land"},   //��ΪWild�ı�־��������
        { "MagicBugMove","hit"},   //��ΪWild�ı�־�ƶ�����
        { "BonusDefault","animation"},   //����FreeSpinģʽ��ͼ��Ĭ�ϴ�������
        { "BoostTrigger","animation"},   //FreeSpin���ٽ�����������
        { "WinTrigger","animation"},   //FreeSpin��ý�����������
        { "GuideBoxAnim","animation" },  //��ʾ�򶯻���

        //�ȴ�Сҳ��
        { "CompareSize_CleopatraAnim_win","hit" },  //ѡ��J����޺󶯻���
        { "CompareSize_CleopatraAnim_fail","wish" },  //ûѡ��J����޺󶯻���
        { "CompareSize_CleopatraAnim_idle","idle" }     //�޺��idle����
    };
}

