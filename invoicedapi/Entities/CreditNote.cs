using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class CreditNote : AbstractEntity<CreditNote> {

		internal CreditNote() : base()
		{
			this.EntityName = "/credit_notes";
		}

		internal CreditNote(Connection conn) : base(conn) {
			this.EntityName = "/credit_notes";
		}

		protected override string EntityId() {
			return this.Id.ToString();
		}

		protected override bool HasVoid() {
			return true;
		}

		protected override bool HasAttachments() {
			return true;
		}

		protected override bool HasSends() {
			return true;
		}

		[JsonProperty("id")]
		public long? Id { get; set; }

		[JsonProperty("object")]
		public string Obj { get; set; }

		[JsonProperty("customer")]
		public long? Customer { get; set; }

		[JsonProperty("invoice")]
		public long? Invoice { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("draft")]
		public bool? Draft { get; set; }

		[JsonProperty("closed")]
		public bool? Closed { get; set; }

		[JsonProperty("paid")]
		public bool? Paid { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("number")]
		public string Number { get; set; }

		[JsonProperty("date")]
		public long? Date { get; set; }

		[JsonProperty("items")]
		public IList<LineItem> Items { get; set; }

		[JsonProperty("notes")]
		public string Notes { get; set; }

		[JsonProperty("subtotal")]
		public long? Subtotal { get; set; }

		[JsonProperty("discounts")]
		public IList<Discount> Discounts { get; set; }

		[JsonProperty("taxes")]
		public IList<Tax> Taxes { get; set; }

		[JsonProperty("total")]
		public long? Total { get; set; }

		[JsonProperty("balance")]
		public long? Balance { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("pdf_url")]
		public string PdfUrl { get; set; }

		[JsonProperty("created_at")]
		public long? CreatedAt { get; set; }

		[JsonProperty("metadata")]
		public Metadata Metadata { get; set; }

		[JsonProperty("attachments")]
		public IList<long> Attachments { get; set; }

		[JsonProperty("calculate_taxes")]
		public bool? CalculateTaxes { get; set; }
		
		[JsonProperty("ship_to")]
		public object ShipTo { get; set; }

		// Conditional Serialisation
		
		public bool ShouldSerializeId() {
			return false;
		}

		public bool ShouldSerializeObj() {
			return false;
		}

		public bool ShouldSerializeCustomer() {
			if (this.CurrentOperation != "Create") return false;
			return true;
		}

		public bool ShouldSerializeInvoice() {
			if (this.CurrentOperation != "Create") return false;
			return true;
		}

		public bool ShouldSerializePaid() {
			return false;
		}

		public bool ShouldSerializeStatus() {
			return false;
		}

		public bool ShouldSerializeSubtotal() {
			return false;
		}

		public bool ShouldSerializeTotal() {
			return false;
		}

		public bool ShouldSerializeBalance() {
			return false;
		}

		public bool ShouldSerializeUrl() {
			return false;
		}

		public bool ShouldSerializePdfUrl() {
			return false;
		}

		public bool ShouldSerializeCreatedAt() {
			return false;
		}

	}

}
