
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
public class Transaction : Entity<Transaction>

{


	internal Transaction(Connection conn) : base(conn) {
	}

	override public long getEntityID() {
		return this.id;
	}

	override public string getEntityName() {
		return "estimates";
	}

	override public bool hasCRUD() {
		return true;

	}

	override public bool hasList() {
		return false;
	}


	[JsonProperty("id")]
	public int id { get; set; }

	[JsonProperty("object")]
	public string object2 { get; set; }

	[JsonProperty("customer")]
	public int customer { get; set; }

	[JsonProperty("invoice")]
	public int invoice { get; set; }

	[JsonProperty("credit_note")]
	public object credit_note { get; set; }

	[JsonProperty("date")]
	public int date { get; set; }

	[JsonProperty("type")]
	public string type { get; set; }

	[JsonProperty("method")]
	public string method { get; set; }

	[JsonProperty("status")]
	public string status { get; set; }

	[JsonProperty("gateway")]
	public object gateway { get; set; }

	[JsonProperty("gateway_id")]
	public object gateway_id { get; set; }

	[JsonProperty("payment_source")]
	public object payment_source { get; set; }

	[JsonProperty("currency")]
	public string currency { get; set; }

	[JsonProperty("amount")]
	public int amount { get; set; }

	[JsonProperty("notes")]
	public object notes { get; set; }

	[JsonProperty("parent_transaction")]
	public object parent_transaction { get; set; }

	[JsonProperty("pdf_url")]
	public string pdf_url { get; set; }

	[JsonProperty("created_at")]
	public int created_at { get; set; }

	[JsonProperty("metadata")]
	public Metadata metadata { get; set; }
}

}
