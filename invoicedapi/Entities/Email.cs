using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Email : Item
	{
		
		public Email() : base() {

		}

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("state")]
		public string State { get; set; }

		[JsonProperty("reject_reason")]
		public string RejectReason { get; set; }

		[JsonProperty("email")]
		public string EMail { get; set; } // must have diff name than enclosing type; accomplished through archaic capitalization of email

        [JsonProperty("template")]
		public string template { get; set; }

        [JsonProperty("subject")]
		public string subject { get; set; }

        [JsonProperty("message")]
		public string Message { get; set; }

        [JsonProperty("opens")]
		public int opens { get; set; }

        [JsonProperty("opens_detail")]
		public object OpensDetail { get; set; }

        [JsonProperty("created_at")]
		public long CreatedAt { get; set; }

		override public string EntityId() {
			return this.Id.ToString();
		}
		
	}

}
