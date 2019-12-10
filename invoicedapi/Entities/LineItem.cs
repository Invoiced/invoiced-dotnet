
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

    public class LineItem : AbstractItem
    {

        public LineItem() : base() {
            
        }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("object")]
        public string Obj { get; set; }

        [JsonProperty("catalog_item")]
        public string CatalogItem { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("unit_cost")]
        public long UnitCost { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("discountable")]
        public bool Discountable { get; set; }

        [JsonProperty("discounts")]
        public IList<Discount> Discounts { get; set; }

        [JsonProperty("taxable")]
        public bool Taxable { get; set; }

        [JsonProperty("taxes")]
        public IList<Tax> Taxes { get; set; }

        [JsonProperty("plan")]
		public string Plan { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        protected override string EntityId() {
			return this.Id.ToString();
		}
    
    }

}
