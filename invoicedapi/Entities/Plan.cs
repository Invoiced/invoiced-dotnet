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

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("unit_cost")]
		public long UnitCost { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("taxable")]
		public bool Taxable { get; set; }

		[JsonProperty("taxes")]
		public Tax[] Taxes { get; set; }

		[JsonProperty("avalara_tax_code")]
		public string AvalaraTaxCode { get; set; }

		[JsonProperty("gl_account")]
		public string GLAccount { get; set; }

		[JsonProperty("discountable")]
		public bool Discountable { get; set; }
		
		[JsonProperty("created_at")]
		public long CreatedAt { get; set; }

		[JsonProperty("metadata")]
		public Metadata Metadata { get; set; }

	}

}

