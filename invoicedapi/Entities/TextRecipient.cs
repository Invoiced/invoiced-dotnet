using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class TextRecipient : AbstractItem
	{
		
		public TextRecipient() : base() {

		}

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("phone")]
		public string Phone { get; set; }


		protected override string EntityId() {
			return "TextRecipient";
			// this is only used for json heading in ToString()
		}
		
	}

}
