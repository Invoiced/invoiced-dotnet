using Newtonsoft.Json;

namespace Invoiced
{
    public class LetterRequest : AbstractItem
    {
        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("start")] public long? Start { get; set; }

        [JsonProperty("end")] public long End { get; set; }

        [JsonProperty("items")] public string Items { get; set; }

        protected override string EntityId()
        {
            return "LetterRequest";
            // this is only used for json heading in ToString()
        }
    }
}