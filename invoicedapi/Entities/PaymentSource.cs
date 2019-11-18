using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class PaymentSource : Item
	{
		
		public PaymentSource() : base() {

		}

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("object")]
		public string Obj { get; set; }

		[JsonProperty("brand")]
		public string Brand { get; set; }

		[JsonProperty("last4")]
		public string Last4 { get; set; }

		[JsonProperty("exp_month")]
		public long ExpMonth { get; set; }

		[JsonProperty("exp_year")]
		public long ExpYear { get; set; }

		[JsonProperty("funding")]
		public string Funding { get; set; }

		public override string EntityId() {
			return this.Id.ToString();
		}

	}

}
