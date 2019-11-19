using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class CatalogItem : AbstractEntity<CatalogItem> {


		internal CatalogItem(Connection conn) : base(conn) {
		}

		protected override string EntityIdString() {
			return this.Id;
		}

		public override string EntityName() {
			return "catalog_items";
		}

		public override bool HasStringId() {
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
		public IList<Tax> Taxes { get; set; }

		[JsonProperty("avalara_tax_code")]
		public string AvalaraTaxCode { get; set; }

		[JsonProperty("gl_account")]
		public string GlAccount { get; set; }

		[JsonProperty("discountable")]
		public bool Discountable { get; set; }
		
		[JsonProperty("created_at")]
		public long CreatedAt { get; set; }

		[JsonProperty("metadata")]
		public Metadata Metadata { get; set; }

		// Conditional Serialisation

		public bool ShouldSerializeId() {
			return this.CurrentOperation == "Create";
		}
		
		public bool ShouldSerializeObj() {
			return false;
		}

		public bool ShouldSerializeCurrency() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeUnitCost() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeTaxable() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeTaxes() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeAvalaraTaxCode() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeGlAccount() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeDiscountable() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeCreatedAt() {
			return false;
		}

	}

}
