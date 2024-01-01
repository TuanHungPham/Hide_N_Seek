using Newtonsoft.Json;

public class AchievementBaseData : BaseData
{
    public long achievementData;

    public void AddData(long achievementData)
    {
        this.achievementData = achievementData;
    }

    public override void ParseToData(string json)
    {
        AchievementBaseData achievementBaseData = JsonConvert.DeserializeObject<AchievementBaseData>(json);
        AddData(achievementBaseData.achievementData);
    }
}