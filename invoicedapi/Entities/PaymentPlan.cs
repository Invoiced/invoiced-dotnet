
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class PaymentPlan : AbstractEntity<PaymentPlan>
	{

		internal long InvoiceId;

		public PaymentPlan(Connection conn, long InvoiceId) : base(conn) {
			this.InvoiceId = InvoiceId;
		}
		
		public PaymentPlan() : base(){

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

		public override bool HasCRUD() {
			return true;
		}

		public override bool HasList() {
			return false;
		}

		public override bool HasStringId() {
			return false;
		}

		public override bool IsSubEntity() {
			return true;
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

	}
	
}
