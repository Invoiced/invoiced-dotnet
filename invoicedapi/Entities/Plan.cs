using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Plan : AbstractEntity<Plan> {


		internal Plan() : base() {
			this.EntityName = "/plans";
		}
		
		internal Plan(Connection conn) : base(conn) {
			this.EntityName = "/plans";
		}

		protected override string EntityId() {
			return this.Id;
		}

		public virtual bool HasStringId() {
			return true;
		}

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("object")]
		public string Obj { get; set; }

        [JsonProperty("catalog_item")]
		public string Item { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("amount")]
		public double? Amount { get; set; }

        [JsonProperty("pricing_mode")]
		public string PricingMode { get; set; }

        [JsonProperty("quantity_type")]
		public string QuantityType { get; set; }

        [JsonProperty("interval")]
		public string Interval { get; set; }

        [JsonProperty("interval_count")]
		public long? IntervalCount { get; set; }

        [JsonProperty("tiers")]
		public object Tiers { get; set; }

		[JsonProperty("created_at")]
		public long? CreatedAt { get; set; }

		[JsonProperty("metadata")]
		public Metadata Metadata { get; set; }

		// Conditional Serialisation

		public bool ShouldSerializeId() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeObj() {
			return false;
		}

		public bool ShouldSerializeItem() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeCurrency() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeAmount() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializePricingMode() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeQuantityType() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeInterval() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeIntervalCount() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeTiers() {
			return this.CurrentOperation == "Create";
		}

	}

}
