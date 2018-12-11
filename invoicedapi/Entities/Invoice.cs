using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
public class Invoice : Entity<Invoice>
{

	public Invoice(Connection conn) : base(conn) {
	}

	public Invoice() : base(){

	}

	override public long EntityID() {
		return this.Id;
	}

	override public string EntityName() {
		return "invoices";
	}

	override public bool HasCRUD() {
		return true;

	}

	override public bool HasList() {
		return true;
	}


	[JsonProperty("id")]
	public int Id { get; }

	[JsonProperty("object")]
	public string Object2 { get; set; }

	[JsonProperty("customer")]
	public int Customer { get; set; }

	[JsonProperty("name")]
	public object Name { get; set; }

	[JsonProperty("currency")]
	public string Currency { get; set; }

	[JsonProperty("draft")]
	public bool Draft { get; set; }

	[JsonProperty("closed")]
	public bool Closed { get; set; }

	[JsonProperty("paid")]
	public bool Paid { get; set; }

	[JsonProperty("status")]
	public string Status { get; set; }

	[JsonProperty("chase")]
	public bool Chase { get; set; }

	[JsonProperty("next_chase_on")]
	public object NextChaseOn { get; set; }

	[JsonProperty("autopay")]
	public bool Autopay { get; set; }

	[JsonProperty("attempt_count")]
	public int AttemptCount { get; set; }

	[JsonProperty("next_payment_attempt")]
	public object NextPaymentAttempt { get; set; }

	[JsonProperty("subscription")]
	public object Subscription { get; set; }

	[JsonProperty("number")]
	public string Number { get; set; }

	[JsonProperty("date")]
	public int Date { get; set; }

	[JsonProperty("due_date")]
	public int? DueDate { get; set; }

	[JsonProperty("payment_terms")]
	public string PaymentTerms { get; set; }

	[JsonProperty("items")]
	public IList<LineItem> Items { get; set; }

	[JsonProperty("notes")]
	public object Notes { get; set; }

	[JsonProperty("subtotal")]
	public int Subtotal { get; set; }

	[JsonProperty("discounts")]
	public IList<object> Discounts { get; set; }

	[JsonProperty("taxes")]
	public IList<Tax> Taxes { get; set; }

	[JsonProperty("total")]
	public double Total { get; set; }

	[JsonProperty("balance")]
	public double Balance { get; set; }

	[JsonProperty("url")]
	public string Url { get; set; }

	[JsonProperty("payment_url")]
	public string PaymentUrl { get; set; }

	[JsonProperty("pdf_url")]
	public string PdfUrl { get; set; }

	[JsonProperty("created_at")]
	public int CreatedAt { get; set; }

	[JsonProperty("metadata")]
	public  Metadata Metadata { get; set; }

}


public class Metadata
{

}

public class LineItem
{

	[JsonProperty("id")]
	public int Id { get; set; }

	[JsonProperty("object")]
	public string Object2 { get; set; }

	[JsonProperty("catalog_item")]
	public string CatalogItem { get; set; }

	[JsonProperty("type")]
	public string Type { get; set; }

	[JsonProperty("name")]
	public string Name { get; set; }

	[JsonProperty("description")]
	public object Description { get; set; }

	[JsonProperty("quantity")]
	public int Quantity { get; set; }

	[JsonProperty("unit_cost")]
	public int UnitCost { get; set; }

	[JsonProperty("amount")]
	public int Amount { get; set; }

	[JsonProperty("discountable")]
	public bool Discountable { get; set; }

	[JsonProperty("discounts")]
	public IList<object> Discounts { get; set; }

	[JsonProperty("taxable")]
	public bool Taxable { get; set; }

	[JsonProperty("taxes")]
	public IList<object> Taxes { get; set; }

	[JsonProperty("metadata")]
	public Metadata Metadata { get; set; }
}

public class Tax
{
	[JsonProperty("id")]
	public int Id { get; set; }

	[JsonProperty("object")]
	public string Object2 { get; set; }

	[JsonProperty("amount")]
	public double Amount { get; set; }

	[JsonProperty("tax_rate")]
	public object TaxRate { get; set; }
}

}
