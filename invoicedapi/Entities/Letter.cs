using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Letter : AbstractItem
	{
		
		public Letter() : base() {

		}

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("state")]
		public string State { get; set; }

        [JsonProperty("num_pages")]
		public long NumPages { get; set; }

        [JsonProperty("expected_delivery_date")]
		public long ExpectedDeliveryDate { get; set; }

        [JsonProperty("to")]
		public string To { get; set; }

        [JsonProperty("created_at")]
		public long CreatedAt { get; set; }

		public override string EntityId() {
			return this.Id.ToString();
		}
		
	}

}
