using Newtonsoft.Json;

namespace Invoiced
{
    public class BalanceHistory : AbstractItem
    {
        [JsonProperty("timestamp")] public long? Timestamp { get; set; }

        [JsonProperty("currency")] public string Currency { get; set; }

        [JsonProperty("balance")] public double? Balance { get; set; }

        protected override string EntityId()
        {
            return "BalanceHistory";
            // this is only used for json heading in ToString()
        }
    }
}