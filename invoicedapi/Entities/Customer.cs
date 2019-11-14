using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{


public class Customer : Entity<Customer>
{
	internal Customer(Connection conn) : base(conn) {
	}

	public Customer() : base() {

	}

	override public long EntityID() {
		return this.Id;
	}

	override public string EntityIDString() {
		throw new EntityException(this.EntityName() + " ID type is long, not string");
	}

	override public string EntityName() {
		return "customers";
	}

	override public bool HasCRUD() {
		return true;
	}

	override public bool HasList() {
		return true;
	}

	override public bool HasStringID() {
		return false;
	}

	public bool ShouldSerializeId() {
        return false;
    }

	public bool ShouldSerializeCreateAt() {
        return false;
    }

	[JsonProperty("id")]
	public long Id { get; set; }

	[JsonProperty("object")]
	public Object Object2 { get; set; }

	[JsonProperty("name")]
	public string Name { get; set; }

	[JsonProperty("number")]
	public string Number { get; set; }

	[JsonProperty("email")]
	public string Email { get; set; }

	[JsonProperty("autopay")]
	public bool Autopay { get; set; }

	[JsonProperty("autopay_delay_days")]
	public long AutopayDelayDays { get; set; }

	[JsonProperty("payment_terms")]
	public string PaymentTerms { get; set; }

	[JsonProperty("payment_source")]
	public PaymentSource PaymentSource { get; set; }

	[JsonProperty("taxable")]
	public bool Taxable { get; set; }

	[JsonProperty("taxes")]
	public IList<object> Taxes { get; set; }

	[JsonProperty("type")]
	public string Type { get; set; }

	[JsonProperty("attention_to")]
	public string AttentionTo { get; set; }

	[JsonProperty("address1")]
	public string Address1 { get; set; }

	[JsonProperty("address2")]
	public string Address2 { get; set; }

	[JsonProperty("city")]
	public string City { get; set; }

	[JsonProperty("state")]
	public string State { get; set; }

	[JsonProperty("postal_code")]
	public string PostalCode { get; set; }

	[JsonProperty("country")]
	public string Country { get; set; }

	[JsonProperty("language")]
	public string Language { get; set; }

	[JsonProperty("chase")]
	public bool Chase { get; set; }

	[JsonProperty("chasing_cadence")]
	public long ChasingCadence { get; set; }

	[JsonProperty("next_chase_step")]
	public long NextChaseStep { get; set; }

	[JsonProperty("tax_id")]
	public string TaxId { get; set; }

	[JsonProperty("avalara_entity_use_code")]
	public string AvalaraEntityUseCode { get; set; }

	[JsonProperty("avalara_exemption_number")]
	public string AvalaraExemptionNumber { get; set; }

	[JsonProperty("phone")]
	public string Phone { get; set; }

	[JsonProperty("credit_hold")]
	public bool CreditHold { get; set; }

	[JsonProperty("credit_limit")]
	public long CreditLimit { get; set; }

	[JsonProperty("owner")]
	public long Owner { get; set; }

	[JsonProperty("parent_customer")]
	public long ParentCustomer { get; set; }

	[JsonProperty("notes")]
	public string Notes { get; set; }

	[JsonProperty("sign_up_page")]
	public string SignUpPage { get; set; }

	[JsonProperty("sign_up_url")]
	public string SignupUrl { get; set; }

	[JsonProperty("statement_pdf_url")]
	public string StatementPdfUrl { get; set; }

	[JsonProperty("created_at")]
	public long CreatedAt { get; set; }

	[JsonProperty("metadata")]
	public Metadata Metadata { get; set; }

}

}
