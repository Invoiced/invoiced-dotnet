using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Invoice : Entity<Invoice>
	{

		public Invoice(Connection conn) : base(conn) {
		}

		public Invoice() : base(){

		}

		override public long EntityID() {
			return this.Id;
		}

		override public string EntityIDString() {
			return this.Id.ToString();
		}

		override public string EntityName() {
			return "invoices";
		}

		override public bool HasCRUD() {
			return true;
		}

		override public bool HasList() {
			return true;
		}

		override public bool HasStringID() {
			return false;
		}

		[JsonProperty("id")]
		public long Id { get;set; }

		[JsonProperty("object")]
		public string Object { get; set; }

		[JsonProperty("customer")]
		public long Customer { get; set; }

		[JsonProperty("name")]
		public object Name { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("draft")]
		public bool Draft { get; set; }

		[JsonProperty("closed")]
		public bool Closed { get; set; }

		[JsonProperty("paid")]
		public bool Paid { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("chase")]
		public bool Chase { get; set; }

		[JsonProperty("next_chase_on")]
		public object NextChaseOn { get; set; }

		[JsonProperty("autopay")]
		public bool Autopay { get; set; }

		[JsonProperty("attempt_count")]
		public long AttemptCount { get; set; }

		[JsonProperty("next_payment_attempt")]
		public object NextPaymentAttempt { get; set; }

		[JsonProperty("subscription")]
		public object Subscription { get; set; }

		[JsonProperty("number")]
		public string Number { get; set; }

		[JsonProperty("date")]
		public long Date { get; set; }

		[JsonProperty("due_date")]
		public long? DueDate { get; set; }

		[JsonProperty("payment_terms")]
		public string PaymentTerms { get; set; }

		[JsonProperty("items")]
		public IList<LineItem> Items { get; set; }

		[JsonProperty("notes")]
		public object Notes { get; set; }

		[JsonProperty("subtotal")]
		public double Subtotal { get; set; }

		[JsonProperty("discounts")]
		public IList<object> Discounts { get; set; }

		[JsonProperty("taxes")]
		public IList<Tax> Taxes { get; set; }

		[JsonProperty("total")]
		public double Total { get; set; }

		[JsonProperty("balance")]
		public double Balance { get; set; }

		[JsonProperty("payment_plan")]
		public long PaymentPlan { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("payment_url")]
		public string PaymentUrl { get; set; }

		[JsonProperty("pdf_url")]
		public string PdfUrl { get; set; }

		[JsonProperty("created_at")]
		public long CreatedAt { get; set; }

		[JsonProperty("metadata")]
		public Metadata Metadata { get; set; }

		[JsonProperty("ship_to")]
		public object ShipTo { get; set; }

		[JsonProperty("attachments")]
		public IList<long> Attachments { get; set; }

		[JsonProperty("calculate_taxes")]
		public bool CalculateTaxes { get; set; }

		[JsonProperty("disabled_payment_methods")]
		public IList<string> DisabledPaymentMethods { get; set; }

	}

}
