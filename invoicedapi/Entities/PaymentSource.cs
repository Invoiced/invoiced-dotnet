using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
public class PaymentSource 
{


	[JsonProperty("id")]
	public int Id { get; set; }

	[JsonProperty("object")]
	public string Object2 { get; set; }

	[JsonProperty("brand")]
	public string Brand { get; set; }

	[JsonProperty("last4")]
	public string Last4 { get; set; }

	[JsonProperty("exp_month")]
	public int ExpMonth { get; set; }

	[JsonProperty("exp_year")]
	public int ExpYear { get; set; }

	[JsonProperty("funding")]
	public string Funding { get; set; }
}
}
