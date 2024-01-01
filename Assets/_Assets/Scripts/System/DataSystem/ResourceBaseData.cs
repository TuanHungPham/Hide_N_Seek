using Newtonsoft.Json;

public class ResourceBaseData : BaseData
{
    public long resourceData;

    public void AddData(long resourceData)
    {
        this.resourceData = resourceData;
    }

    public override void ParseToData(string json)
    {
        ResourceBaseData resourceBaseData = JsonConvert.DeserializeObject<ResourceBaseData>(json);
        AddData(resourceBaseData.resourceData);
    }
}