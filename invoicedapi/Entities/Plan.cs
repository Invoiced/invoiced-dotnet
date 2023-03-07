using Newtonsoft.Json;

namespace Invoiced
{
    public class Plan : AbstractEntity<Plan>
    {
        internal Plan()
        {
            EntityName = "/plans";
        }

        internal Plan(Connection conn) : base(conn)
        {
            EntityName = "/plans";
        }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("catalog_item")] public string Item { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("currency")] public string Currency { get; set; }

        [JsonProperty("amount")] public double? Amount { get; set; }

        [JsonProperty("pricing_mode")] public string PricingMode { get; set; }

        [JsonProperty("quantity_type")] public string QuantityType { get; set; }

        [JsonProperty("interval")] public string Interval { get; set; }

        [JsonProperty("interval_count")] public long? IntervalCount { get; set; }

        [JsonProperty("tiers")] public object Tiers { get; set; }

        [JsonProperty("created_at")] public long? CreatedAt { get; set; }

        [JsonProperty("updated_at")] public long? UpdatedAt { get; set; }

        [JsonProperty("metadata")] public Metadata Metadata { get; set; }

        protected override string EntityId()
        {
            return Id;
        }

        public virtual bool HasStringId()
        {
            return true;
        }

        // Conditional Serialisation
        public bool ShouldSerializeId()
        {
            return CurrentOperation == "Create";
        }

        public bool ShouldSerializeObject()
        {
            return false;
        }

        public bool ShouldSerializeItem()
        {
            return CurrentOperation == "Create";
        }

        public bool ShouldSerializeCurrency()
        {
            return CurrentOperation == "Create";
        }

        public bool ShouldSerializeAmount()
        {
            return CurrentOperation == "Create";
        }

        public bool ShouldSerializePricingMode()
        {
            return CurrentOperation == "Create";
        }

        public bool ShouldSerializeQuantityType()
        {
            return CurrentOperation == "Create";
        }

        public bool ShouldSerializeInterval()
        {
            return CurrentOperation == "Create";
        }

        public bool ShouldSerializeIntervalCount()
        {
            return CurrentOperation == "Create";
        }

        public bool ShouldSerializeTiers()
        {
            return CurrentOperation == "Create";
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