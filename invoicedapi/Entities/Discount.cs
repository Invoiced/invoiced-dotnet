using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Discount : AbstractItem
	{
		public Discount() : base(){

		}

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("object")]
		public string Obj { get; set; }

		[JsonProperty("amount")]
		public long Amount { get; set; }

		[JsonProperty("coupon")]
		public Coupon Coupon { get; set; }

		[JsonProperty("expires")]
		public long? Expires { get; set; }

		protected override string EntityId() {
			return this.Id.ToString();
		}

	}

}
