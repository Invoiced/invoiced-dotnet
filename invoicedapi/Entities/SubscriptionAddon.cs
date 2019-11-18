using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
    public class SubscriptionAddon : AbstractItem
	{
		public SubscriptionAddon() : base(){

		}

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("object")]
        public string Obj { get; set; }

        [JsonProperty("plan")]
        public string Plan { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        public override string EntityId() {
			return this.Id.ToString();
		}

    }
}