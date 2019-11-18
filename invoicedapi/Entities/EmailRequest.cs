using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class EmailRequest : Item
	{
		
		public EmailRequest() : base() {

		}

		[JsonProperty("to")]
		public EmailRecipient[] to { get; set; }

		[JsonProperty("bcc")]
		public string Bcc { get; set; }

        [JsonProperty("subject")]
		public string Subject { get; set; }

        [JsonProperty("message")]
		public string Message { get; set; }

        [JsonProperty("type")]
		public string Type { get; set; }

        [JsonProperty("start")]
		public long Start { get; set; }

        [JsonProperty("end")]
		public long End { get; set; }

        [JsonProperty("items")]
		public string Items { get; set; }

		override public string EntityID() {
			throw new EntityException("Email requests have no ID");
		}
		
	}

}
