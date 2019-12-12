
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class PaymentPlan : AbstractEntity<PaymentPlan>
	{

		public PaymentPlan(Connection conn) : base(conn) {
			this.EntityName = "/payment_plan";
		}
		
		public PaymentPlan() : base() {
			this.EntityName = "/payment_plan";
		}

		protected override string EntityId() {
			return this.Id.ToString();
		}

		protected override bool HasList() {
			return false;
		}

		[JsonProperty("id")]
		public long? Id { get; set; }

		[JsonProperty("object")]
		public string Obj { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("installments")]
		public IList<Installment> Installments { get; set; }

		[JsonProperty("approval")]
		public Approval Approval { get; set; }

		[JsonProperty("created_at")]
		public long? CreatedAt { get; set; }

		// identical to default Delete() but does not append ID to end of URL
		public void Cancel() {
			this.GetConnection().Delete(this.GetEndpoint(false));
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
