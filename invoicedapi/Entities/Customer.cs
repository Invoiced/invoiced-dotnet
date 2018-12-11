using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{


public class Customer : Entity<Customer>
{
	internal Customer(Connection conn) : base(conn) {
	}

	public Customer() : base(){

	}

	override public long EntityID() {
		return this.Id;
	}

	override public string EntityName() {
		return "customers";
	}

	override public bool HasCRUD() {
		return true;

	}

	override public bool HasList() {
		return false;
	}

	public bool ShouldSerializeId()
    {
        return false;
    }

	public bool ShouldSerializeCreateAt()
    {
        return false;
    }

	[JsonProperty("id")]
	public int Id { get; set; }

	[JsonProperty("object")]
	public string Object2 { get; set; }

	[JsonProperty("name")]
	public string Name { get; set; }

	[JsonProperty("number")]
	public string Number { get; set; }

	[JsonProperty("email")]
	public string Email { get; set; }

	[JsonProperty("autopay")]
	public bool Autopay { get; set; }

	[JsonProperty("payment_terms")]
	public object PaymentTerms { get; set; }

	[JsonProperty("payment_source")]
	public PaymentSource PaymentSource { get; set; }

	[JsonProperty("taxes")]
	public IList<object> Taxes { get; set; }

	[JsonProperty("type")]
	public string Type { get; set; }

	[JsonProperty("attention_to")]
	public object AttentionTo { get; set; }

	[JsonProperty("address1")]
	public object Address1 { get; set; }

	[JsonProperty("address2")]
	public object Address2 { get; set; }

	[JsonProperty("city")]
	public object City { get; set; }

	[JsonProperty("state")]
	public object State { get; set; }

	[JsonProperty("postal_code")]
	public object PostalCode { get; set; }

	[JsonProperty("country")]
	public string Country { get; set; }

	[JsonProperty("tax_id")]
	public object TaxId { get; set; }

	[JsonProperty("phone")]
	public object Phone { get; set; }

	[JsonProperty("notes")]
	public object Notes { get; set; }

	[JsonProperty("sign_up_page")]
	public object SignUpPage { get; set; }

	[JsonProperty("sign_up_url")]
	public object SignupUrl { get; set; }

	[JsonProperty("statement_pdf_url")]
	public string StatementPdfUrl { get; set; }

	[JsonProperty("created_at")]
	public int CreatedAt { get; set; }

	[JsonProperty("metadata")]
	public Metadata Metadata { get; set; }


}
}
