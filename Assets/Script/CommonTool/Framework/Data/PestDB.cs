//
// Auto Generated Code By excel2json
// https://neil3d.gitee.io/coding/excel2json.html
// 1. 每个 Sheet 形成一个 Struct 定义, Sheet 的名称作为 Struct 的名称
// 2. 表格约定：第一行是变量名称，第二行是变量类型

// Generate From GameSetting.xlsx

using System;
using System.Collections.Generic;

namespace zeta_framework
{

public class GameSettingDB
{
	public string id; // 配置名称
	public string value; // 配置的值
	public string Miami_Rain; // 属性类型
	public string Upright; // 注释
}

public class ItemDB
{
	public string id; // 资源ID(名称)
	public string Miami_Rain; // 属性类型
	public string Upright; // 注释
	public string Roar; // 图标
	public int AmmoniaMuddy; // 默认值
	public int ButMuddy; // 最小值
	public int LugMuddy; // 最大值
	public int type; // 资源类型(1、消耗类、2、经验类)
}

public class ItemGroupDB
{
	public string id; // 资源组ID
	public string Fire_id; // 资源ID
	public int Fire_Rib; // 资源数量
}

public class ShopDB
{
	public string id; // 商店ID
	public string Euphrates_He; // 对应ItemGroup表中的id
	public string gp_Due; // GooglePlay的pid
	public string Oat_Due; // AppStore的pid
	public string Envy_Roar; // 商品图标
	public string Plain; // 商品名称
	public int Clinical_Rain; // 购买类型：1:现金；2:金币;3:钻石
	public double Gamma; // 价格
	public bool An_Thin; // 是否在商店中展示
	public int num; // 数量（每日限购）
}

public class ExpBoxDB
{
	public string AHe_He; // 宝箱类型/活动id
	public int Gourd; // 经验宝箱等级
	public string Sod_Jay; // 升级所需资源
	public int Sod_Miami; // 升级所需资源值
	public string Euphrates_He; // 奖励(对应ItemGroup表的id)
	public string Fire_id; // 奖励(对应Item表的id)
	public int Fire_Miami; // 奖励值
}

public class SkinDB
{
	public string Fire_id; // 皮肤对应的itemID
	public string Tire_Rain; // 皮肤分类
	public int Encode_Rain; // 解锁类型，1:经验自动解锁;2:金币购买;3:现金购买;4:自定义解锁
	public string Encode_Miami; // 解锁条件值
}

public class ActivityDB
{
	public string id; // 活动名称
	public string Miami_Rain; // 属性类型
	public string Upright; // 注释
	public int Encode_Gourd; // 解锁关卡
	public int Viral_Fork; // 第一次活动开始时间的时间戳
	public int Slippery; // 活动的持续时间（秒）
	public int Hanker; // 每期活动开始时间的间隔（秒）
	public int Bottle; // 活动的期数（-1:无限期）
	public int Viral_Rain; // 开始方式
	public string Indeed; // 活动图标
	public string Quick; // 活动prefab
	public bool Pick_Percussive; // 活动结束自动结算
	public bool Engrave; // 两期活动是否可重叠
	public string Pick_Cite_Fork; // 自动打开弹窗时机
	public int Pick_Cite_Ballroom; // 弹出打开优先级
}

public class ActivityDailyGiftDB
{
	public int DNA; // 第几天
	public string Euphrates_He; // 奖励资源组id
	public string Fire_id; // 奖励资源id
	public int Fire_Rib; // 奖励数量
}

public class ActivityEndlessTreasureDB
{
	public string id; // ID
	public string Euphrates_He; // 奖励资源组id
	public string Fire_id; // 奖励资源id
	public int Fire_Rib; // 奖励数量
	public string Envy_He; // 商店ID
	public string color; // UI背景色
}

public class RankDB
{
	public string Move_He; // 榜单ID
	public string Comprise_He; // 活动ID
	public string Fire_id; // 榜单资源ID
	public int Fire_Rib_Rain; // 资源累计类型
	public bool Aloft_Fire; // 清榜后是否清空资源
	public int Lug_Vibrant; // 榜单显示前n名
}

public class RankRewardDB
{
	public string Move_He; // 榜单ID
	public int But_Move; // 最小排名
	public int Lug_Move; // 最大排名
	public string Euphrates_He; // 奖励id
	public int Fire_Rib; // 获得奖励所需资源最小数量
}


/// <summary>
/// 1. 资源类为名为'Bust'的Sheet中的配置
/// 2. 表格约定：id为属性名称，value_type为属性类型，comment为注释
/// Generate From GameSetting.xlsx -> Sheet[Bust]
/// </summary>
public class ResourceDB
{
	public Bust Wood{ get; set; } // 金币
	public Bust Neither{ get; set; } // 钻石
	public Bust Carbon{ get; set; } // 体力
	public Bust Develop_Carbon{ get; set; } // 无限体力
	public Bust Sod{ get; set; } // 经验
	public Bust Grasshopper_Much{ get; set; } // 连胜
	public Bust remove_Ax{ get; set; } // 免广告
	public Bust Tire_My_1{ get; set; } // 皮肤1
	public Bust Spectrum{ get; set; } // 金箔
	public Bust Thus{ get; set; } // 星星
}

/// <summary>
/// 1. 资源类为名为'GameSetting'的Sheet中的配置
/// 2. 表格约定：id为属性名称，value_type为属性类型，comment为注释
/// Generate From GameSetting.xlsx -> Sheet[GameSetting]
/// </summary>
public class SettingDB
{
	public int Carbon_Reliable_Together{ get; set; } // 体力恢复时间间隔
	public int Carbon_cost{ get; set; } // 每关体力消耗
	public int Loss_lv_Midnight_Officially{ get; set; } // 全部关卡都通过后奖励策略-宝箱（0、不给奖励；1：按最后一关奖励；2：从第一级循环）
	public string ArtistRegretHay{ get; set; } // 内购 - google公钥
	public string AppleStewBoil{ get; set; } // 内购 - Apple证书
}

/// <summary>
/// 1. 资源类为名为'Terylene'的Sheet中的配置
/// 2. 表格约定：id为属性名称，value_type为属性类型，comment为注释
/// Generate From GameSetting.xlsx -> Sheet[Terylene]
/// </summary>
public class ActivityCtrlDB
{
	public TeryleneGessoPainAmid GessoPain{ get; set; } // 签到奖励
	public TeryleneJumbleNoAmid JumbleNo{ get; set; } // 去广告
	public Terylene LifeJet{ get; set; } // 金箔宝箱
	public TeryleneInquireSouthernAmid InquireSouthern{ get; set; } // 无尽宝藏
	public Terylene OpenBeam{ get; set; } // 星星排行榜
}

}
// End of Auto Generated Code
