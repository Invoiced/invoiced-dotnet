using Newtonsoft.Json;

namespace Invoiced
{
    public class Discount : AbstractItem
    {
        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("amount")] public double? Amount { get; set; }

        [JsonProperty("coupon")] public Coupon Coupon { get; set; }

        [JsonProperty("expires")] public long? Expires { get; set; }

        protected override string EntityId()
        {
            return Id.ToString();
        }
    }
}