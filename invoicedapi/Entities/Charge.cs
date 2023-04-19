using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Invoiced
{
    public class Charge : AbstractEntity<Charge>
    {
        internal Charge(Connection conn) : base(conn)
        {
            EntityName = "/charges";
        }

        public Charge()
        {
            EntityName = "/charges";
        }

        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("customer")] public long? Customer { get; set; }

        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("gateway")] public string Gateway { get; set; }

        [JsonProperty("gateway_id")] public string GatewayId { get; set; }

        [JsonProperty("payment_source")] public PaymentSource PaymentSource { get; set; }

        [JsonProperty("currency")] public string Currency { get; set; }

        [JsonProperty("amount")] public double? Amount { get; set; }

        [JsonProperty("failure_message")] public string FailureMessage { get; set; }

        [JsonProperty("amount_refunded")] public double? AmountRefunded { get; set; }

        [JsonProperty("refunded")] public bool? Refunded { get; set; }

        [JsonProperty("refunds")] public IList<Refund> Refunds { get; set; }

        [JsonProperty("disputed")] public bool? Disputed { get; set; }

        [JsonProperty("created_at")] public long? CreatedAt { get; set; }

        [JsonProperty("updated_at")] public long? UpdatedAt { get; set; }

        protected override string EntityId()
        {
            return Id.ToString();
        }

        public Payment Create(ChargeRequest chargeRequest)
        {
            var url = "/charges";
            var jsonRequestBody = chargeRequest.ToJsonString();

            var responseText = GetConnection().Post(url, null, jsonRequestBody);

            try
            {
                return JsonConvert.DeserializeObject<Payment>(responseText,
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
        public async Task<Payment> CreateAsync(ChargeRequest chargeRequest)
        {
            var url = "/charges";
            var jsonRequestBody = chargeRequest.ToJsonString();

            var responseText = await GetConnection().PostAsync(url, null, jsonRequestBody);

            try
            {
                return JsonConvert.DeserializeObject<Payment>(responseText,
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