
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Installment : AbstractItem
	{
		
		public Installment() : base() {

		}

		[JsonProperty("id")]
		public long? Id { get; set; }

		[JsonProperty("date")]
		public long? Date { get; set; }

		[JsonProperty("amount")]
		public long? Amount { get; set; }

		[JsonProperty("balance")]
		public long? Balance { get; set; }

		protected override string EntityId() {
			return this.Id.ToString();
		}
		
	}

}
