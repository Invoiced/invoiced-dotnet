using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Estimate : Entity<Estimate>
	{

		public Estimate() : base(){

		}

		internal Estimate(Connection conn) : base(conn) {
		}

		override public long EntityID() {
			return this.Id;
		}

		override public string EntityIDString() {
			throw new EntityException(this.EntityName() + " ID type is long, not string");
		}

		override public string EntityName() {
			return "estimates";
		}

		override public bool HasCRUD() {
			return true;
		}

		override public bool HasList() {
			return false;
		}

		override public bool HasStringID() {
			return false;
		}

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("object")]
		public string Object2 { get; set; }

		[JsonProperty("customer")]
		public int Customer { get; set; }

		[JsonProperty("invoice")]
		public long Invoice { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("draft")]
		public bool Draft { get; set; }

		[JsonProperty("closed")]
		public bool Closed { get; set; }

		[JsonProperty("approved")]
		public bool Approved { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("number")]
		public string Number { get; set; }

		[JsonProperty("date")]
		public int Date { get; set; }

		[JsonProperty("expiration_date")]
		public long ExpirationDate { get; set; }

		[JsonProperty("payment_terms")]
		public string PaymentTerms { get; set; }

		[JsonProperty("items")]
		public IList<LineItem> Items { get; set; }

		[JsonProperty("notes")]
		public string Notes { get; set; }

		[JsonProperty("subtotal")]
		public int Subtotal { get; set; }

		[JsonProperty("discounts")]
		public IList<object> Discounts { get; set; }

		[JsonProperty("taxes")]
		public IList<Tax> Taxes { get; set; }

		[JsonProperty("total")]
		public double Total { get; set; }

		[JsonProperty("deposit")]
		public int Deposit { get; set; }

		[JsonProperty("deposit_paid")]
		public bool DepositPaid { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("pdf_url")]
		public string PdfUrl { get; set; }

		[JsonProperty("created_at")]
		public int CreatedAt { get; set; }

		[JsonProperty("metadata")]
		public Metadata Metadata { get; set; }

		[JsonProperty("calculate_taxes")]
		public bool CalculateTaxes { get; set; }

		[JsonProperty("disabled_payment_methods")]
		public IList<string> DisabledPaymentMethods { get; set; }

	}

}