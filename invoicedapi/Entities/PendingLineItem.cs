using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
    public class PendingLineItem : AbstractEntity<PendingLineItem>
    {
        public PendingLineItem(Connection conn) : base(conn)
        {
            EntityName = "/line_items";
        }

        public PendingLineItem()
        {
            EntityName = "/line_items";
        }

        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("catalog_item")] public string Item { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("quantity")] public double? Quantity { get; set; }

        [JsonProperty("amount")] public double? Amount { get; set; }

        [JsonProperty("unit_cost")] public double? UnitCost { get; set; }

        [JsonProperty("discountable")] public bool? Discountable { get; set; }

        [JsonProperty("discounts")] public IList<Discount> Discounts { get; set; }

        [JsonProperty("taxable")] public bool? Taxable { get; set; }

        [JsonProperty("taxes")] public IList<Tax> Taxes { get; set; }

        [JsonProperty("metadata")] public Metadata Metadata { get; set; }

        protected override string EntityId()
        {
            return Id.ToString();
        }

        // Conditional Serialisation

        public bool ShouldSerializeId()
        {
            return false;
        }

        public bool ShouldSerializeItem()
        {
            return CurrentOperation == nameof(Create) || CurrentOperation == nameof(CreateAsync);
        }
    }
}