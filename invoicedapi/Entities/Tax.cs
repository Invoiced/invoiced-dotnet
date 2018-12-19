using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
public class Tax : Item
{
    public Tax() : base(){

	}

    [JsonProperty("id")]
	public long Id { get; set; }

	[JsonProperty("object")]
	public string Object { get; set; }

	[JsonProperty("amount")]
	public double Amount { get; set; }

	[JsonProperty("tax_rate")]
	public Rate TaxRate { get; set; }

    override public long EntityID() {
		return this.Id;
	}

}

}