using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class PendingLineItem : AbstractEntity<PendingLineItem>
	{

		public PendingLineItem(Connection conn) : base(conn) {
			this.EntityName = "/line_items";
		}

		public PendingLineItem() : base(){
			this.EntityName = "/line_items";
		}

		protected override string EntityId() {
			return this.Id.ToString();
		}

		[JsonProperty("id")]
		public long? Id { get; set; }

		[JsonProperty("catalog_item")]
		public string Item { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

        [JsonProperty("description")]
		public string Description { get; set; }

        [JsonProperty("quantity")]
		public double? Quantity { get; set; }

        [JsonProperty("amount")]
		public double? Amount { get; set; }

        [JsonProperty("unit_cost")]
		public double? UnitCost { get; set; }

        [JsonProperty("discountable")]
		public bool? Discountable { get; set; }

        [JsonProperty("discounts")]
		public IList<Discount> Discounts { get; set; }

		[JsonProperty("taxable")]
		public bool? Taxable { get; set; }

		[JsonProperty("taxes")]
		public IList<Tax> Taxes { get; set; }

		[JsonProperty("plan")]
		public string Plan { get; set; }

		[JsonProperty("metadata")]
		public Metadata Metadata { get; set; }

		// Conditional Serialisation

		public bool ShouldSerializeId() {
			return false;
		}

		public bool ShouldSerializeItem() {
			return this.CurrentOperation == "Create";
		}

	}

}