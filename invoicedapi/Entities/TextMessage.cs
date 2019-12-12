using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class TextMessage : AbstractItem
	{
		
		public TextMessage() : base() {

		}

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("state")]
		public string State { get; set; }

        [JsonProperty("to")]
		public string To { get; set; }

        [JsonProperty("message")]
		public string Message { get; set; }

        [JsonProperty("created_at")]
		public long? CreatedAt { get; set; }

		protected override string EntityId() {
			return this.Id;
		}
		
	}

}
