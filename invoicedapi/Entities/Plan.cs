using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Plan : AbstractEntity<Plan> {


		internal Plan(Connection conn) : base(conn) {
		}

		public override long EntityId() {
			throw new EntityException(this.EntityName() + " ID type is string, not long");
		}

		public override string EntityIdString() {
			return this.Id;
		}

		public override string EntityName() {
			return "plans";
		}

		public override bool HasStringId() {
			return true;
		}

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("object")]
		public string Obj { get; set; }

        [JsonProperty("catalog_item")]
		public string CatalogItem { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("amount")]
		public long Amount { get; set; }

        [JsonProperty("pricing_mode")]
		public string PricingMode { get; set; }

        [JsonProperty("quantity_type")]
		public string QuantityType { get; set; }

        [JsonProperty("interval")]
		public string Interval { get; set; }

        [JsonProperty("interval_count")]
		public long IntervalCount { get; set; }

        [JsonProperty("tiers")]
		public object Tiers { get; set; }

		[JsonProperty("created_at")]
		public long CreatedAt { get; set; }

		[JsonProperty("metadata")]
		public Metadata Metadata { get; set; }

		// Conditional Serialisation

		public bool ShouldSerializeId() {
			if (this.CurrentOperation != "Create") return false;
			return true;
		}

		public bool ShouldSerializeObj() {
			return false;
		}

		public bool ShouldSerializeCatalogItem() {
			if (this.CurrentOperation != "Create") return false;
			return true;
		}

		public bool ShouldSerializeCurrency() {
			if (this.CurrentOperation != "Create") return false;
			return true;
		}

		public bool ShouldSerializeAmount() {
			if (this.CurrentOperation != "Create") return false;
			return true;
		}

		public bool ShouldSerializePricingMode() {
			if (this.CurrentOperation != "Create") return false;
			return true;
		}

		public bool ShouldSerializeQuantityType() {
			if (this.CurrentOperation != "Create") return false;
			return true;
		}

		public bool ShouldSerializeInterval() {
			if (this.CurrentOperation != "Create") return false;
			return true;
		}

		public bool ShouldSerializeIntervalCount() {
			if (this.CurrentOperation != "Create") return false;
			return true;
		}

		public bool ShouldSerializeTiers() {
			if (this.CurrentOperation != "Create") return false;
			return true;
		}

	}

}
