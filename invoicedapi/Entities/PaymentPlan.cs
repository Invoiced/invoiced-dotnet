
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class PaymentPlan : Item
	{

		public PaymentPlan() : base(){

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

		public override string EntityId() {
			return this.Id.ToString();
		}

	}
	
}
