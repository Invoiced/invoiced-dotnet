using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
	public class Invoice : AbstractDocument<Invoice>
	{
		public Invoice(Connection conn) : base(conn) {
			this.EntityName = "/invoices";
		}

		public Invoice() : base(){
			this.EntityName = "/invoices";
		}

		[JsonProperty("paid")]
		public bool? Paid { get; set; }

		[JsonProperty("autopay")]
		public bool? Autopay { get; set; }

		[JsonProperty("attempt_count")]
		public long? AttemptCount { get; set; }

		[JsonProperty("next_payment_attempt")]
		public long? NextPaymentAttempt { get; set; }

		[JsonProperty("subscription")]
		public long? Subscription { get; set; }

		[JsonProperty("due_date")]
		public long? DueDate { get; set; }

		[JsonProperty("payment_terms")]
		public string PaymentTerms { get; set; }

		[JsonProperty("balance")]
		public double? Balance { get; set; }

		[JsonProperty("payment_plan")]
		public long? PaymentPlan { get; set; }

		[JsonProperty("payment_url")]
		public string PaymentUrl { get; set; }

		[JsonProperty("ship_to")]
		public object ShipTo { get; set; }

		public PaymentPlan NewPaymentPlan() {
			PaymentPlan paymentPlan = new PaymentPlan(this.GetConnection());
			paymentPlan.SetEndpointBase(this.GetEndpoint(true));
			return paymentPlan;
		}

		public Note NewNote() {
			Note note = new Note(this.GetConnection());
			note.SetEndpointBase(this.GetEndpoint(true));
			note.InvoiceId = this.Id;
			return note;
		}

		public void Pay() {
			string url = this.GetEndpoint(true) + "/pay";

			string responseText = this.GetConnection().Post(url,null,"");
			
			try {
				JsonConvert.PopulateObject(responseText,this);
			} catch(Exception e) {
				throw new EntityException("",e);
			}
		}

		// Conditional Serialisation
		
		public bool ShouldSerializePaid() {
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

		public bool ShouldSerializeBalance() {
			return false;
		}

		public bool ShouldSerializePaymentPlan() {
			return false;
		}

		public bool ShouldSerializePaymentUrl() {
			return false;
		}
	}
}
