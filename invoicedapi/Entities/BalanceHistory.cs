using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class BalanceHistory : AbstractItem
	{
		
		public BalanceHistory() : base() {

		}

		[JsonProperty("timestamp")]
		public long Timestamp { get; set; }

		[JsonProperty("balance")]
		public long Balance { get; set; }

		public override string EntityId() {
			return "BalanceHistory";
            // this is only used for json heading in ToString()
		}
		
	}

}
