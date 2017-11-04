using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{


public class Customer : Entity<Customer>
{
	internal Customer(Connection conn) : base(conn) {
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

	[JsonProperty("name")]
	public string name { get; set; }

	[JsonProperty("number")]
	public string number { get; set; }

	[JsonProperty("email")]
	public string email { get; set; }

	[JsonProperty("autopay")]
	public bool autopay { get; set; }

	[JsonProperty("payment_terms")]
	public object payment_terms { get; set; }

	[JsonProperty("payment_source")]
	public PaymentSource payment_source { get; set; }

	[JsonProperty("taxes")]
	public IList<object> taxes { get; set; }

	[JsonProperty("type")]
	public string type { get; set; }

	[JsonProperty("attention_to")]
	public object attention_to { get; set; }

	[JsonProperty("address1")]
	public object address1 { get; set; }

	[JsonProperty("address2")]
	public object address2 { get; set; }

	[JsonProperty("city")]
	public object city { get; set; }

	[JsonProperty("state")]
	public object state { get; set; }

	[JsonProperty("postal_code")]
	public object postal_code { get; set; }

	[JsonProperty("country")]
	public string country { get; set; }

	[JsonProperty("tax_id")]
	public object tax_id { get; set; }

	[JsonProperty("phone")]
	public object phone { get; set; }

	[JsonProperty("notes")]
	public object notes { get; set; }

	[JsonProperty("sign_up_page")]
	public object sign_up_page { get; set; }

	[JsonProperty("sign_up_url")]
	public object sign_up_url { get; set; }

	[JsonProperty("statement_pdf_url")]
	public string statement_pdf_url { get; set; }

	[JsonProperty("created_at")]
	public int created_at { get; set; }

	[JsonProperty("metadata")]
	public Metadata metadata { get; set; }


}
}
