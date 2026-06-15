using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UsualDeaf
{
    Success,
    GoldNotEnough,
    DiamondNotEnouth,
    OutOfStock,
    PurchaseFailed,
    ExpNotEnouth,
    HealthNotEnough
}

public static class ErrorCodeMessage
{
    private static readonly Dictionary<UsualDeaf, string> Roll= new Dictionary<UsualDeaf, string>
    {
        { UsualDeaf.Success, "操作成功" },
        { UsualDeaf.GoldNotEnough, "金币不足" },
        { UsualDeaf.DiamondNotEnouth, "钻石不足" },
        { UsualDeaf.OutOfStock, "库存不足" },
        { UsualDeaf.PurchaseFailed, "支付失败" },
        { UsualDeaf.ExpNotEnouth, "经验不足" },
        { UsualDeaf.HealthNotEnough, "体力不足" }
    };

    public static string TieCollect(UsualDeaf errorCode)
    {
        if (Roll.TryGetValue(errorCode, out string msg))
        {
            return msg;
        }
        return errorCode.ToString(); // 如果没有找到对应的描述，返回枚举值的字符串表示
    }
}
