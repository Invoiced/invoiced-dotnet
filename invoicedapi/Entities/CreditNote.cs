using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

public class CreditNote : Entity<CreditNote> {


	internal CreditNote(Connection conn) : base(conn) {
	}

	override public long getEntityID() {
		return this.id;
	}

	override public string getEntityName() {
		return "customers";
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

	[JsonProperty("name")]
	public object name { get; set; }

	[JsonProperty("currency")]
	public string currency { get; set; }

	[JsonProperty("draft")]
	public bool draft { get; set; }

	[JsonProperty("closed")]
	public bool closed { get; set; }

	[JsonProperty("paid")]
	public bool paid { get; set; }

	[JsonProperty("status")]
	public string status { get; set; }

	[JsonProperty("number")]
	public string number { get; set; }

	[JsonProperty("date")]
	public int date { get; set; }

	[JsonProperty("items")]
	public IList<LineItem> items { get; set; }

	[JsonProperty("notes")]
	public object notes { get; set; }

	[JsonProperty("subtotal")]
	public int subtotal { get; set; }

	[JsonProperty("discounts")]
	public IList<object> discounts { get; set; }

	[JsonProperty("taxes")]
	public IList<Tax> taxes { get; set; }

	[JsonProperty("total")]
	public double total { get; set; }

	[JsonProperty("balance")]
	public double balance { get; set; }

	[JsonProperty("url")]
	public string url { get; set; }

	[JsonProperty("pdf_url")]
	public string pdf_url { get; set; }

	[JsonProperty("created_at")]
	public int created_at { get; set; }

	[JsonProperty("metadata")]
	public  Metadata metadata { get; set; }

}


}

