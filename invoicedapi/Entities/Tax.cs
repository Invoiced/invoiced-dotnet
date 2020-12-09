using Newtonsoft.Json;

namespace Invoiced
{
    public class Tax : AbstractItem
    {
        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("amount")] public double? Amount { get; set; }

        [JsonProperty("tax_rate")] public TaxRate TaxRate { get; set; }

        protected override string EntityId()
        {
            return Id.ToString();
        }
    }
}