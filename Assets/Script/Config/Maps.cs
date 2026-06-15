using System.Collections.Generic;

/// <summary>
/// 映射
/// </summary>
public class Maps
{
    /// <summary>
    /// spine动画名映射
    /// </summary>
    public static Dictionary<string, string> spineAnimNameMap = new Dictionary<string, string>() 
    {
        //Slots格子
        { "WindDefault","animation"},   //万能标志默认触发动画
        { "CleopatraDefault","animation"},   //高级图标1默认触发动画
        { "AnkhDefault","animation"},   //高级图标2默认触发动画
        { "HonusDefault","animation"},   //高级图标3默认触发动画
        { "JarDefault","animation"},   //中级图标1默认触发动画
        { "RingDefault","animation"},   //中级图标2默认触发动画
        { "TenDefault","10"},   //低级图标1默认触发动画
        { "JDefault","j"},   //低级图标2默认触发动画
        { "QDefault","q"},   //低级图标3默认触发动画
        { "KDefault","k"},   //低级图标4默认触发动画
        { "ADefault","a"},   //低级图标5默认触发动画
        { "ScratchDefault","animation"},   //刮刮卡默认触发动画
        { "ScatterDefault","animation"},   //Scatter默认触发动画
        { "LuckyWheelDefault","animation"},   //幸运转盘默认触发动画
        { "MagicBugTrigger","land"},   //变为Wild的标志触发动画
        { "MagicBugMove","hit"},   //变为Wild的标志移动动画
        { "BonusDefault","animation"},   //触发FreeSpin模式的图标默认触发动画
        { "BoostTrigger","animation"},   //FreeSpin销毁奖励触发动画
        { "WinTrigger","animation"},   //FreeSpin获得奖励触发动画
        { "GuideBoxAnim","animation" },  //提示框动画名

        //比大小页面
        { "CompareSize_CleopatraAnim_win","hit" },  //选到J后的艳后动画名
        { "CompareSize_CleopatraAnim_fail","wish" },  //没选到J后的艳后动画名
        { "CompareSize_CleopatraAnim_idle","idle" }     //艳后的idle动画
    };
}

