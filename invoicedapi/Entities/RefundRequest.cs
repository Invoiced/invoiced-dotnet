using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class RefundRequest : Item
	{
		
        public RefundRequest(long amount) : base() {
            this.Amount = amount;
        }

		[JsonProperty("amount")]
		public long Amount { get; set; }

		public override string EntityId() {
			return "RefundRequest";
            // this is only used for json heading in ToString()
		}
		
	}

}
