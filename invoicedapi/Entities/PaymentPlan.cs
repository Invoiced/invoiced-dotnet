
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{


public class Installment
{

	[JsonProperty("id")]
	public int Id { get; set; }

	[JsonProperty("date")]
	public int Date { get; set; }

	[JsonProperty("amount")]
	public int Amount { get; set; }

	[JsonProperty("balance")]
	public int Balance { get; set; }
}

public class Approval
{

	[JsonProperty("id")]
	public int Id { get; set; }

	[JsonProperty("ip")]
	public string Ip { get; set; }

	[JsonProperty("timestamp")]
	public int Timestamp { get; set; }

	[JsonProperty("user_agent")]
	public string UserAgent { get; set; }
}

public class PaymentPlan
{
	[JsonProperty("id")]
	public int Id { get; set; }

	[JsonProperty("object")]
	public string Object2 { get; set; }

	[JsonProperty("status")]
	public string Status { get; set; }

	[JsonProperty("installments")]
	public IList<Installment> Installments { get; set; }

	[JsonProperty("approval")]
	public Approval Approval { get; set; }

	[JsonProperty("created_at")]
	public int CreatedAt { get; set; }

}
}