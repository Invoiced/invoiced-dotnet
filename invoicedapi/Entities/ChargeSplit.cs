using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class ChargeSplit : Item
	{
		
		public ChargeSplit() : base() {

		}

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("invoice")]
		public long Invoice { get; set; }

		[JsonProperty("amount")]
		public long Amount { get; set; }

		public override string EntityId() {
			return "ChargeSplit";
            // this is only used for json heading in ToString()
		}
		
	}

}
