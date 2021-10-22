using Newtonsoft.Json;

namespace Invoiced
{
    public class TextMessage : AbstractItem
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("state")] public string State { get; set; }

        [JsonProperty("to")] public string To { get; set; }

        [JsonProperty("message")] public string Message { get; set; }

        [JsonProperty("created_at")] public long? CreatedAt { get; set; }

        [JsonProperty("updated_at")] public long? UpdatedAt { get; set; }

        protected override string EntityId()
        {
            return Id;
        }
    }
}