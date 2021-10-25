using Newtonsoft.Json;

namespace Invoiced
{
    public class Coupon : AbstractEntity<Coupon>
    {
        internal Coupon()
        {
            EntityName = "/coupons";
        }

        internal Coupon(Connection conn) : base(conn)
        {
            EntityName = "/coupons";
        }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("currency")] public string Currency { get; set; }

        [JsonProperty("value")] public double? Value { get; set; }

        [JsonProperty("is_percent")] public bool? IsPercent { get; set; }

        [JsonProperty("exclusive")] public bool? Exclusive { get; set; }

        [JsonProperty("durationo")] public long? Duration { get; set; }

        [JsonProperty("expiration_date")] public long? ExpirationDate { get; set; }

        [JsonProperty("max_redemptions")] public long? MaxRedemptions { get; set; }

        [JsonProperty("created_at")] public long? CreatedAt { get; set; }

        [JsonProperty("updated_at")] public long? UpdatedAt { get; set; }

        [JsonProperty("metadata")] public Metadata Metadata { get; set; }

        protected override string EntityId()
        {
            return Id;
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

        public bool ShouldSerializeCurrency()
        {
            return CurrentOperation == "Create";
        }

        public bool ShouldSerializeValue()
        {
            return CurrentOperation == "Create";
        }

        public bool ShouldSerializeIsPercent()
        {
            return CurrentOperation == "Create";
        }

        public bool ShouldSerializeExclusive()
        {
            return CurrentOperation == "Create";
        }

        public bool ShouldSerializeExpirationDate()
        {
            return CurrentOperation == "Create";
        }

        public bool ShouldSerializeMaxRedemptions()
        {
            return CurrentOperation == "Create";
        }

        public bool ShouldSerializeCreatedAt()
        {
            return false;
        }
    }
}