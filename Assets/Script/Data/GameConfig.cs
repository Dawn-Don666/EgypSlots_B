using LitJson;
using System.Collections.Generic;

/// <summary>
/// 游戏所使用的配置信息
/// </summary>
public class GameConfig
{
    public int wildAddWeight;
    public int maxLuckyWheelEnergy;
    public int maxSpinCount;
    public List<SpecialRewardsItem> specialRewardsItems;
    public Dictionary<string, JackpotDataItem> jackpotData;
    public CompareSizeData compareSizeData;
    public OpenBoxData openBoxData;
    public Match3Data match3Data;
    public ScratchData scratchData;
    public LuckyWheelData luckyWheelData;
    public GoldPigData goldPigData;
}
