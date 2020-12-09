using Newtonsoft.Json;

namespace Invoiced
{
    public class ShippingDetail : AbstractItem
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("attention_to")] public string AttentionTo { get; set; }

        [JsonProperty("address1")] public string Address1 { get; set; }

        [JsonProperty("address2")] public string Address2 { get; set; }

        [JsonProperty("city")] public string City { get; set; }

        [JsonProperty("state")] public string State { get; set; }

        [JsonProperty("postal_code")] public string PostalCode { get; set; }

        [JsonProperty("country")] public string Country { get; set; }

        protected override string EntityId()
        {
            return "ShippingDetail";
            // this is only used for json heading in ToString()
        }
    }
}