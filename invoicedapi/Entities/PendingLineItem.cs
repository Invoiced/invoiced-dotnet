using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class PendingLineItem : Entity<PendingLineItem>
	{

		public PendingLineItem(Connection conn) : base(conn) {
		}

		public PendingLineItem() : base(){

		}

		public override long EntityId() {
			return this.Id;
		}

		public override string EntityIdString() {
			return this.Id.ToString();
		}

		public override string EntityName() {
			return "line_items";
		}

		public override bool HasCRUD() {
			return true;
		}

		public override bool HasList() {
			return true;
		}

		public override bool HasStringId() {
			return false;
		}

		[JsonProperty("id")]
		public long Id { get; set; }

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

        [JsonProperty("amount")]
		public long Amount { get; set; }

        [JsonProperty("unit_cost")]
		public long UnitCost { get; set; }

        [JsonProperty("discountable")]
		public bool Discountable { get; set; }

        [JsonProperty("discounts")]
		public Discount[] Discounts { get; set; }

		[JsonProperty("taxable")]
		public bool Taxable { get; set; }

		[JsonProperty("taxes")]
		public Tax[] Taxes { get; set; }

		[JsonProperty("plan")]
		public string Plan { get; set; }

		[JsonProperty("metadata")]
		public Metadata Metadata { get; set; }

	}

}