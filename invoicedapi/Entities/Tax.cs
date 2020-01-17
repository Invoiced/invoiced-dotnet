using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Tax : AbstractItem
	{

		public Tax() : base(){

		}

		[JsonProperty("id")]
		public long? Id { get; set; }

		[JsonProperty("object")]
		public string Obj { get; set; }

		[JsonProperty("amount")]
		public double? Amount { get; set; }

		[JsonProperty("tax_rate")]
		public TaxRate TaxRate { get; set; }

		protected override string EntityId() {
			return this.Id.ToString();
		}

	}

}
