using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Balance : AbstractItem
	{
		
		public Balance() : base() {

		}

		[JsonProperty("available_credits")]
		public long AvailableCredits { get; set; }

		[JsonProperty("history")]
		public IList<BalanceHistory> History { get; set; }

		[JsonProperty("past_due")]
		public bool PastDue { get; set; }

		[JsonProperty("total_outstanding")]
		public long TotalOutstanding { get; set; }

		protected override string EntityId() {
			return "Balance";
            // this is only used for json heading in ToString()
		}
		
	}

}
