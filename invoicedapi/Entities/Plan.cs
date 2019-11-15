using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Plan : Entity<Plan> {


		internal Plan(Connection conn) : base(conn) {
		}

		override public long EntityID() {
			throw new EntityException(this.EntityName() + " ID type is string, not long");
		}

		override public string EntityIDString() {
			return this.Id;
		}

		override public string EntityName() {
			return "plans";
		}

		override public bool HasCRUD() {
			return true;
		}
		
		override public bool HasList() {
			return true;
		}

		override public bool HasStringID() {
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

	}

}
