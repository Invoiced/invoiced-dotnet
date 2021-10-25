using Newtonsoft.Json;

namespace Invoiced
{
    public class CreditBalanceAdjustment : AbstractEntity<CreditBalanceAdjustment>
    {
        internal CreditBalanceAdjustment(Connection conn) : base(conn)
        {
            EntityName = "/credit_balance_adjustments";
        }

        public CreditBalanceAdjustment()
        {
            EntityName = "/credit_balance_adjustments";
        }

        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }
        
        [JsonProperty("date")] public long? Date { get; set; }

        [JsonProperty("customer")] public long? Customer { get; set; }

        [JsonProperty("currency")] public string Currency { get; set; }

        [JsonProperty("amount")] public double? Amount { get; set; }

        [JsonProperty("notes")] public string Notes { get; set; }

        [JsonProperty("created_at")] public long? CreatedAt { get; set; }

        [JsonProperty("updated_at")] public long? UpdatedAt { get; set; }

        protected override string EntityId()
        {
            return Id.ToString();
        }

        // Conditional Serialisation
        public bool ShouldSerializeId()
        {
            return false;
        }

        public bool ShouldSerializeObject()
        {
            return false;
        }

        public bool ShouldSerializeCreatedAt()
        {
            return false;
        }
    }
}