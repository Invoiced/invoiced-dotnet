using Newtonsoft.Json;

namespace Invoiced
{
    public class PaymentSource : AbstractEntity<PaymentSource>
    {
        public PaymentSource()
        {
            EntityName = "/payment_sources";
        }

        internal PaymentSource(Connection conn) : base(conn)
        {
            EntityName = "/payment_sources";
        }

        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("gateway")] public string Gateway { get; set; }

        [JsonProperty("gateway_id")] public string GatewayId { get; set; }

        [JsonProperty("gateway_customer")] public string GatewayCustomer { get; set; }

        [JsonProperty("chargeable")] public bool? Chargeable { get; set; }

        [JsonProperty("failure_reason")] public string FailureReason { get; set; }

        [JsonProperty("receipt_email")] public string ReceiptEmail { get; set; }

        [JsonProperty("created_at")] public long? CreatedAt { get; set; }

        [JsonProperty("updated_at")] public long? UpdatedAt { get; set; }

        protected override bool HasCrud()
        {
            return false;
        }

        protected override string EntityId()
        {
            return Id.ToString();
        }

        public bool ShouldSerializeCreatedAt()
        {
            return false;
        }

        public bool ShouldSerializeUpdatedAt()
        {
            return false;
        }
    }
}