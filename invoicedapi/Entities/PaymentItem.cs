using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
	public class PaymentItem : AbstractItem
	{
		public PaymentItem() : base() {
		}

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("invoice")]
		public long? Invoice { get; set; }

		[JsonProperty("amount")]
		public double? Amount { get; set; }

		protected override string EntityId() {
			return "ChargeSplit";
            // this is only used for json heading in ToString()
		}	
	}
}
