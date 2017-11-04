
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{


public class Installment
{

	[JsonProperty("id")]
	public int id { get; set; }

	[JsonProperty("date")]
	public int date { get; set; }

	[JsonProperty("amount")]
	public int amount { get; set; }

	[JsonProperty("balance")]
	public int balance { get; set; }
}

public class Approval
{

	[JsonProperty("id")]
	public int id { get; set; }

	[JsonProperty("ip")]
	public string ip { get; set; }

	[JsonProperty("timestamp")]
	public int timestamp { get; set; }

	[JsonProperty("user_agent")]
	public string user_agent { get; set; }
}

public class PaymentPlan
{
	[JsonProperty("id")]
	public int id { get; set; }

	[JsonProperty("object")]
	public string object2 { get; set; }

	[JsonProperty("status")]
	public string status { get; set; }

	[JsonProperty("installments")]
	public IList<Installment> installments { get; set; }

	[JsonProperty("approval")]
	public Approval approval { get; set; }

	[JsonProperty("created_at")]
	public int created_at { get; set; }

}
}