using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;

/// <summary>
/// 小工具
/// </summary>
public class GameUtil
{
    /// <summary>
    /// 添加分隔符
    /// </summary>
    /// <param name="num">数字</param>
    /// <returns>加完后的字符串</returns>
    public static string AddDelimiter(int num)
    {
        return num.ToString("N0");
    }

    /// <summary>
    /// 将带有千位分隔符的字符串转换回整数
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int RemoveDelimiter(string str)
    {
        if (string.IsNullOrWhiteSpace(str)) throw new System.Exception("字符串不能为空");

        // 1. 只保留数字和负号
        string core = Regex.Replace(str.Trim(), @"[^0-9\-]", "");

        // 2. 转 int
        return int.TryParse(core, out int v) ? v : throw new System.Exception("字符串不能转换为整数");
    }
}
