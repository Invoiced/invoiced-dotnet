using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class LetterRequest : Item
	{
		
		public LetterRequest() : base() {

		}

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("start")]
		public long Start { get; set; }

        [JsonProperty("end")]
		public long End { get; set; }

        [JsonProperty("items")]
		public string Items { get; set; }

		public override string EntityId() {
			return "LetterRequest";
            // this is only used for json heading in ToString()
		}
		
	}

}
