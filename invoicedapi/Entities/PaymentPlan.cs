
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class PaymentPlan : AbstractEntity<PaymentPlan>
	{

		private long InvoiceId;

		public PaymentPlan(Connection conn, long invoiceId) : base(conn) {
			this.InvoiceId = invoiceId;
		}
		
		public PaymentPlan() : base() {

		}

		protected override string EntityIdString() {
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

			string url = this.Connection.baseUrl() + "/" + this.EntityName();
			
			this.Connection.Delete(url);
		}

		// necessary to override this to avoid appending payment plan ID to DELETE request url
		public override void Delete() {
			this.Cancel();
		}

		// Conditional Serialisation

		public bool ShouldSerializeId() {
			return false;
		}

		public bool ShouldSerializeObj() {
			return false;
		}

		public bool ShouldSerializeStatus() {
			return false;
		}

		public bool ShouldSerializeInstallments() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeApproval() {
			return false;
		}

		public bool ShouldSerializeCreatedAt() {
			return false;
		}

	}
	
}
