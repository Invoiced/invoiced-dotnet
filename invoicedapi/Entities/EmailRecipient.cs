using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class EmailRecipient : Item
	{
		
		public EmailRecipient() : base() {

		}

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }


		override public string EntityID() {
			return "EmailRecipient";
			// this is only used for json heading in ToString()
		}
		
	}

}
