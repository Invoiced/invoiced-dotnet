using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Invoice : AbstractEntity<Invoice>
	{

		public Invoice(Connection conn) : base(conn) {
		}

		public Invoice() : base(){

		}

		public override long EntityId() {
			return this.Id;
		}

		public override string EntityIdString() {
			return this.Id.ToString();
		}

		public override string EntityName() {
			return "invoices";
		}

		public override bool HasVoid() {
			return true;
		}

		public override bool HasAttachments() {
			return true;
		}

		public override bool HasSends() {
			return true;
		}

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("object")]
		public string Obj { get; set; }

		[JsonProperty("customer")]
		public long Customer { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

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
		public long NextChaseOn { get; set; }

		[JsonProperty("autopay")]
		public bool Autopay { get; set; }

		[JsonProperty("attempt_count")]
		public long AttemptCount { get; set; }

		[JsonProperty("next_payment_attempt")]
		public long NextPaymentAttempt { get; set; }

		[JsonProperty("subscription")]
		public long Subscription { get; set; }

		[JsonProperty("number")]
		public string Number { get; set; }

		[JsonProperty("date")]
		public long Date { get; set; }

		[JsonProperty("due_date")]
		public long DueDate { get; set; }

		[JsonProperty("payment_terms")]
		public string PaymentTerms { get; set; }

		[JsonProperty("items")]
		public IList<LineItem> Items { get; set; }

		[JsonProperty("notes")]
		public string Notes { get; set; }

		[JsonProperty("subtotal")]
		public long Subtotal { get; set; }

		[JsonProperty("discounts")]
		public IList<Discount> Discounts { get; set; }

		[JsonProperty("taxes")]
		public IList<Tax> Taxes { get; set; }

		[JsonProperty("total")]
		public long Total { get; set; }

		[JsonProperty("balance")]
		public long Balance { get; set; }

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

		[JsonProperty("attachments")]
		public IList<long> Attachments { get; set; }

		[JsonProperty("calculate_taxes")]
		public bool CalculateTaxes { get; set; }

		[JsonProperty("disabled_payment_methods")]
		public IList<string> DisabledPaymentMethods { get; set; }

		public PaymentPlan NewPaymentPlan() {
			return new PaymentPlan(this.connection, this.Id);
		}

		public Note NewNote() {
			return new Note(this.connection, -1, this.Id);
		}

		public void Pay() {

			string url = this.connection.baseUrl() + "/" + this.EntityName() + "/" + this.EntityIdString() + "/pay";

			string responseText = this.connection.Post(url,null,"");
			
			try {
				JsonConvert.PopulateObject(responseText,this);
			} catch(Exception e) {
				throw new EntityException("",e);
			}

		}

		// Conditional Serialisation
		
		public bool ShouldSerializeId() {
			return false;
		}

		public bool ShouldSerializeObj() {
			return false;
		}

		public bool ShouldSerializeCustomer() {
			if (this.currentOperation != "Create") return false;
			return true;
		}

		public bool ShouldSerializePaid() {
			return false;
		}
		
		public bool ShouldSerializeStatus() {
			return false;
		}

		public bool ShouldSerializeAttemptCount() {
			return false;
		}

		public bool ShouldSerializeNextPaymentAttempt() {
			return false;
		}

		public bool ShouldSerializeSubscription() {
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

		public bool ShouldSerializePaymentPlan() {
			return false;
		}

		public bool ShouldSerializeUrl() {
			return false;
		}

		public bool ShouldSerializePaymentUrl() {
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
