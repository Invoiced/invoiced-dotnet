using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
public class Invoice : Entity<Invoice>
{

	public Invoice(Connection conn) : base(conn) {
	}

	override public long getEntityID() {
		return this.id;
	}

	override public string getEntityName() {
		return "invoices";
	}

	override public bool hasCRUD() {
		return true;

	}

	override public bool hasList() {
		return true;
	}


	[JsonProperty("id")]
	public int id { get; set; }

	[JsonProperty("object")]
	public string object2 { get; set; }

	[JsonProperty("customer")]
	public int customer { get; set; }

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

	[JsonProperty("chase")]
	public bool chase { get; set; }

	[JsonProperty("next_chase_on")]
	public object next_chase_on { get; set; }

	[JsonProperty("autopay")]
	public bool autopay { get; set; }

	[JsonProperty("attempt_count")]
	public int attempt_count { get; set; }

	[JsonProperty("next_payment_attempt")]
	public object next_payment_attempt { get; set; }

	[JsonProperty("subscription")]
	public object subscription { get; set; }

	[JsonProperty("number")]
	public string number { get; set; }

	[JsonProperty("date")]
	public int date { get; set; }

	[JsonProperty("due_date")]
	public int? due_date { get; set; }

	[JsonProperty("payment_terms")]
	public string payment_terms { get; set; }

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

	[JsonProperty("payment_url")]
	public string payment_url { get; set; }

	[JsonProperty("pdf_url")]
	public string pdf_url { get; set; }

	[JsonProperty("created_at")]
	public int created_at { get; set; }

	[JsonProperty("metadata")]
	public  Metadata metadata { get; set; }

}


public class Metadata
{

}

public class LineItem
{

	[JsonProperty("id")]
	public int id { get; set; }

	[JsonProperty("object")]
	public string object2 { get; set; }

	[JsonProperty("catalog_item")]
	public string catalog_item { get; set; }

	[JsonProperty("type")]
	public string type { get; set; }

	[JsonProperty("name")]
	public string name { get; set; }

	[JsonProperty("description")]
	public object description { get; set; }

	[JsonProperty("quantity")]
	public int quantity { get; set; }

	[JsonProperty("unit_cost")]
	public int unit_cost { get; set; }

	[JsonProperty("amount")]
	public int amount { get; set; }

	[JsonProperty("discountable")]
	public bool discountable { get; set; }

	[JsonProperty("discounts")]
	public IList<object> discounts { get; set; }

	[JsonProperty("taxable")]
	public bool taxable { get; set; }

	[JsonProperty("taxes")]
	public IList<object> taxes { get; set; }

	[JsonProperty("metadata")]
	public Metadata metadata { get; set; }
}

public class Tax
{
	[JsonProperty("id")]
	public int id { get; set; }

	[JsonProperty("object")]
	public string object2 { get; set; }

	[JsonProperty("amount")]
	public double amount { get; set; }

	[JsonProperty("tax_rate")]
	public object tax_rate { get; set; }
}

}
