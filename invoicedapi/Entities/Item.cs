using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
    public class Item : AbstractEntity<Item>
    {
        internal Item()
        {
            EntityName = "/items";
        }

        internal Item(Connection conn) : base(conn)
        {
            EntityName = "/items";
        }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("currency")] public string Currency { get; set; }

        [JsonProperty("unit_cost")] public double? UnitCost { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("taxable")] public bool? Taxable { get; set; }

        [JsonProperty("taxes")] public IList<Tax> Taxes { get; set; }

        [JsonProperty("avalara_tax_code")] public string AvalaraTaxCode { get; set; }

        [JsonProperty("avalara_location_code")]
        public string AvalaraLocationCode { get; set; }

        [JsonProperty("gl_account")] public string GlAccount { get; set; }

        [JsonProperty("discountable")] public bool? Discountable { get; set; }

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

        public bool ShouldSerializeUnitCost()
        {
            return CurrentOperation == nameof(Create) || CurrentOperation == nameof(CreateAsync);
        }

        public bool ShouldSerializeTaxable()
        {
            return CurrentOperation == nameof(Create) || CurrentOperation == nameof(CreateAsync);
        }

        public bool ShouldSerializeTaxes()
        {
            return CurrentOperation == nameof(Create) || CurrentOperation == nameof(CreateAsync);
        }

        public bool ShouldSerializeAvalaraTaxCode()
        {
            return CurrentOperation == nameof(Create) || CurrentOperation == nameof(CreateAsync);
        }

        public bool ShouldSerializeGlAccount()
        {
            return CurrentOperation == nameof(Create) || CurrentOperation == nameof(CreateAsync);
        }

        public bool ShouldSerializeDiscountable()
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