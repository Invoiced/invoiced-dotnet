
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Transaction : Entity<Transaction>
	{
	
		internal Transaction(Connection conn) : base(conn) {
		}

		public Transaction() : base(){

		}

		override public long EntityID() {
			return this.Id;
		}

		override public string EntityIDString() {
			return this.Id.ToString();
		}

		override public string EntityName() {
			return "transactions";
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
		public long Id { get; set; }

		[JsonProperty("object")]
		public string object2 { get; set; }

		[JsonProperty("customer")]
		public long Customer { get; set; }

		[JsonProperty("invoice")]
		public long Invoice { get; set; }

		[JsonProperty("credit_note")]
		public object CreditNote { get; set; }

		[JsonProperty("date")]
		public long Date { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("method")]
		public string Method { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("gateway")]
		public string Gateway { get; set; }

		[JsonProperty("gateway_id")]
		public string GatewayId { get; set; }

		[JsonProperty("payment_source")]
		public PaymentSource PaymentSource { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("amount")]
		public int Amount { get; set; }

		[JsonProperty("notes")]
		public string Notes { get; set; }

		[JsonProperty("failure_reason")]
		public string FailureReason { get; set; }

		[JsonProperty("parent_transaction")]
		public long ParentTransaction { get; set; }

		[JsonProperty("pdf_url")]
		public string PdfUrl { get; set; }

		[JsonProperty("created_at")]
		public int CreatedAt { get; set; }

		[JsonProperty("metadata")]
		public Metadata Metadata { get; set; }
	
	}

}
