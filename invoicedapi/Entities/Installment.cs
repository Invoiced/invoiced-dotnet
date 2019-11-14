
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Installment : Item
	{
		
		public Installment() : base() {

		}

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("date")]
		public long Date { get; set; }

		[JsonProperty("amount")]
		public double Amount { get; set; }

		[JsonProperty("balance")]
		public double Balance { get; set; }

		override public long EntityID() {
			return this.Id;
		}

		override public bool HasStringID() {
			return false;
		}
		
	}

}