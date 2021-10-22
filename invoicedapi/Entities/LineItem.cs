using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
    public class LineItem : AbstractItem
    {
        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("catalog_item")] public string Item { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("quantity")] public double? Quantity { get; set; }

        [JsonProperty("unit_cost")] public double? UnitCost { get; set; }

        [JsonProperty("amount")] public double? Amount { get; set; }

        [JsonProperty("discountable")] public bool? Discountable { get; set; }

        [JsonProperty("discounts")] public IList<Discount> Discounts { get; set; }

        [JsonProperty("taxable")] public bool? Taxable { get; set; }

        [JsonProperty("taxes")] public IList<Tax> Taxes { get; set; }

        [JsonProperty("plan")] public string Plan { get; set; }

        [JsonProperty("period_start")] public long? PeriodStart { get; set; }

        [JsonProperty("periood_end")] public long? PeriodEnd { get; set; }

        [JsonProperty("prorated")] public bool? Prorated { get; set; }

        [JsonProperty("subscription")] public long? Subscriptiob { get; set; }

        [JsonProperty("metadata")] public Metadata Metadata { get; set; }

        [JsonProperty("updated_at")] public long? UpdatedAt { get; set; }

        protected override string EntityId()
        {
            return Id.ToString();
        }
    }
}