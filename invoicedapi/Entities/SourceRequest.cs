using Newtonsoft.Json;

namespace Invoiced
{
    public class SourceRequest : AbstractItem
    {
        [JsonProperty("method")] public string Method { get; set; }

        [JsonProperty("make_default")] public bool MakeDefault { get; set; }

        [JsonProperty("invoiced_token")] public string InvoicedToken { get; set; }

        [JsonProperty("gateway_token")] public string GatewayToken { get; set; }

        protected override string EntityId()
        {
            return "SourceRequest";
            // this is only used for json heading in ToString()
        }
    }
}