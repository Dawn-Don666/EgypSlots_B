using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrazeLand
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
    private static readonly Dictionary<CrazeLand, string> Lord= new Dictionary<CrazeLand, string>
    {
        { CrazeLand.Success, "操作成功" },
        { CrazeLand.GoldNotEnough, "金币不足" },
        { CrazeLand.DiamondNotEnouth, "钻石不足" },
        { CrazeLand.OutOfStock, "库存不足" },
        { CrazeLand.PurchaseFailed, "支付失败" },
        { CrazeLand.ExpNotEnouth, "经验不足" },
        { CrazeLand.HealthNotEnough, "体力不足" }
    };

    public static string RatEmbrace(CrazeLand errorCode)
    {
        if (Lord.TryGetValue(errorCode, out string msg))
        {
            return msg;
        }
        return errorCode.ToString(); // 如果没有找到对应的描述，返回枚举值的字符串表示
    }
}
