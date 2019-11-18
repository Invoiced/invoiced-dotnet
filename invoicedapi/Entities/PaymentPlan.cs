
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class PaymentPlan : AbstractEntity<PaymentPlan>
	{

		private long InvoiceId;

		public PaymentPlan(Connection conn, long InvoiceId) : base(conn) {
			this.InvoiceId = InvoiceId;
		}
		
		public PaymentPlan() : base() {

		}

		public override long EntityId() {
			return this.Id;
		}

		public override string EntityIdString() {
			return this.Id.ToString();
		}

		public override string EntityName() {
			return "invoices/" + this.InvoiceId.ToString() + "/payment_plan";
		}

		public override bool HasList() {
			return false;
		}

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("object")]
		public string Obj { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("installments")]
		public IList<Installment> Installments { get; set; }

		[JsonProperty("approval")]
		public Approval Approval { get; set; }

		[JsonProperty("created_at")]
		public long CreatedAt { get; set; }

		// identical to default Delete() but does not append ID to end of URL
		public void Cancel() {
			if (!HasCRUD()) {
				return;
			}

			string url = this.connection.baseUrl() + "/" + this.EntityName();
			
			this.connection.Delete(url);
		}

		// necessary to override this to avoid appending payment plan ID to DELETE request url
		public override void Delete() {
			this.Cancel();
		}

	}
	
}
