using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
public class PaymentSource 
{


	[JsonProperty("id")]
	public int id { get; set; }

	[JsonProperty("object")]
	public string object2 { get; set; }

	[JsonProperty("brand")]
	public string brand { get; set; }

	[JsonProperty("last4")]
	public string last4 { get; set; }

	[JsonProperty("exp_month")]
	public int exp_month { get; set; }

	[JsonProperty("exp_year")]
	public int exp_year { get; set; }

	[JsonProperty("funding")]
	public string funding { get; set; }
}
}
