using System;
using Newtonsoft.Json;

namespace Invoiced
{
    public class Refund : AbstractEntity<Refund>
    {
        internal Refund(Connection conn) : base(conn)
        {
            EntityName = "";
        }

        public Refund()
        {
            EntityName = "";
        }

        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("charge")] public long? Charge { get; set; }

        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("gateway")] public string Gateway { get; set; }

        [JsonProperty("gateway_id")] public string GatewayId { get; set; }

        [JsonProperty("currency")] public string Currency { get; set; }

        [JsonProperty("amount")] public double? Amount { get; set; }

        [JsonProperty("failure_message")] public string FailureMessage { get; set; }

        [JsonProperty("created_at")] public long? CreatedAt { get; set; }

        protected override string EntityId()
        {
            return Id.ToString();
        }

        public Refund Create(long chargeId, double amount)
        {
            var url = GetEndpoint(false) + "/charges/" + chargeId + "/refunds";
            var jsonRequestBody = "{'amount':" + amount + "}";

            var responseText = GetConnection().Post(url, null, jsonRequestBody);

            try
            {
                return JsonConvert.DeserializeObject<Refund>(responseText,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore
                    });
            }
            catch (Exception e)
            {
                throw new EntityException("", e);
            }
        }
    }
}