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
			throw new EntityException("Email recipients have no ID");
		}
		
	}

}
