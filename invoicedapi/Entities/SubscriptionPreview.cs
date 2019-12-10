using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
    public class SubscriptionPreview : AbstractItem
	{
		public SubscriptionPreview() : base(){

		}

        [JsonProperty("first_invoice")]
        public Invoice FirstInvoice { get; set; }

        [JsonProperty("mrr")]
        public long Mrr { get; set; }

        [JsonProperty("recurring_total")]
        public long RecurringTotal { get; set; }

        protected override string EntityId() {
			return "SubscriptionPreview";
            // this is only used for json heading in ToString()
		}

    }
}