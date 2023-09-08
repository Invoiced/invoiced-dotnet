using Newtonsoft.Json;

namespace Invoiced
{
    public class TaxRate : AbstractEntity<TaxRate>
    {
        internal TaxRate()
        {
            EntityName = "/tax_rates";
        }

        internal TaxRate(Connection conn) : base(conn)
        {
            EntityName = "/tax_rates";
        }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("currency")] public string Currency { get; set; }

        [JsonProperty("value")] public double? Value { get; set; }

        [JsonProperty("is_percent")] public bool? IsPercent { get; set; }

        [JsonProperty("inclusive")] public bool? Inclusive { get; set; }

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
            return CurrentOperation == nameof(Create) || CurrentOperation == nameof(CreateAsync);
        }

        public bool ShouldSerializeObject()
        {
            return false;
        }

        public bool ShouldSerializeCurrency()
        {
            return CurrentOperation == nameof(Create) || CurrentOperation == nameof(CreateAsync);
        }

        public bool ShouldSerializeValue()
        {
            return CurrentOperation == nameof(Create) || CurrentOperation == nameof(CreateAsync);
        }

        public bool ShouldSerializeIsPercent()
        {
            return CurrentOperation == nameof(Create) || CurrentOperation == nameof(CreateAsync);
        }

        public bool ShouldSerializeInclusive()
        {
            return CurrentOperation == nameof(Create) || CurrentOperation == nameof(CreateAsync);
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