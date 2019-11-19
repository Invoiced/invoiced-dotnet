using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class TextRequest : AbstractItem
	{
		
		public TextRequest() : base() {

		}

		[JsonProperty("to")]
		public IList<TextRecipient> To { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }

        [JsonProperty("type")]
		public long Type { get; set; }

        [JsonProperty("start")]
		public long Start { get; set; }

        [JsonProperty("end")]
		public long end { get; set; }

        [JsonProperty("items")]
		public string Items { get; set; }

		protected override string EntityId() {
			return "TextRequest";
            // this is only used for json heading in ToString()
		}
		
	}

}
