using Newtonsoft.Json.Linq;

public static class JsonUtil
{
    public static bool JsonEqual(string jsonText1, string jsonText2)
    {
        var json1 = JObject.Parse(jsonText1);
        var json2 = JObject.Parse(jsonText2);

        var areEqual = JToken.DeepEquals(json1, json2);

        return areEqual;
    }
}