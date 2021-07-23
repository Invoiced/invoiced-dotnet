using Newtonsoft.Json;

namespace Invoiced
{
    public class SubscriptionAddon : AbstractItem
    {
        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("amount")] public double? Amount { get; set; }

        [JsonProperty("plan")] public string Plan { get; set; }

        [JsonProperty("quantity")] public long? Quantity { get; set; }

        [JsonProperty("created_at")] public long? CreatedAt { get; set; }

        protected override string EntityId()
        {
            return Id.ToString();
        }
    }
}