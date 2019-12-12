using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class TaxRate : AbstractEntity<TaxRate> {

		internal TaxRate() : base() {
			this.EntityName = "/tax_rates";
		} 

		internal TaxRate(Connection conn) : base(conn) {
			this.EntityName = "/tax_rates";
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

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("value")]
		public long? Value { get; set; }

        [JsonProperty("is_percent")]
		public bool? IsPercent { get; set; }

        [JsonProperty("inclusive")]
		public bool? Inclusive { get; set; }

		[JsonProperty("created_at")]
		public long? CreatedAt { get; set; }

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

		public bool ShouldSerializeCurrency() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeValue() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeIsPercent() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeInclusive() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeCreatedAt() {
			return false;
		}

	}

}
