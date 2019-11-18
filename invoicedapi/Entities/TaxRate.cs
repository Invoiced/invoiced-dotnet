using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class TaxRate : Entity<TaxRate> {


		internal TaxRate(Connection conn) : base(conn) {
		}

		public override long EntityId() {
			throw new EntityException(this.EntityName() + " ID type is string, not long");
		}

		public override string EntityIdString() {
			return this.Id;
		}

		public override string EntityName() {
			return "tax_rates";
		}

		public override bool HasCRUD() {
			return true;
		}
		
		public override bool HasList() {
			return true;
		}

		public override bool HasStringId() {
			return true;
		}

		public override bool IsSubEntity() {
			return false;
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
		public long Value { get; set; }

        [JsonProperty("is_percent")]
		public bool IsPercent { get; set; }

        [JsonProperty("inclusive")]
		public bool Exclusive { get; set; }

		[JsonProperty("created_at")]
		public long CreatedAt { get; set; }

		[JsonProperty("metadata")]
		public Metadata Metadata { get; set; }

	}

}
