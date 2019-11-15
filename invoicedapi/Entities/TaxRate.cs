using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class TaxRate : Entity<TaxRate> {


		internal TaxRate(Connection conn) : base(conn) {
		}

		override public long EntityID() {
			throw new EntityException(this.EntityName() + " ID type is string, not long");
		}

		override public string EntityIDString() {
			return this.Id;
		}

		override public string EntityName() {
			return "tax_rates";
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

