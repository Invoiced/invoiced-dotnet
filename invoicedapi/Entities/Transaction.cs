
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
public class Transaction : Entity<Transaction>

{
	internal Transaction(Connection conn) : base(conn) {
	}

	public Transaction() : base(){

	}

	public bool ShouldSerializeId()
    {
        return false;
    }

	override public long EntityID() {
		return this.Id;
	}

	override public string EntityName() {
		return "estimates";
	}

	override public bool HasCRUD() {
		return true;

	}

	override public bool HasList() {
		return true;
	}


	[JsonProperty("id")]
	public int Id { get; set; }

	[JsonProperty("object")]
	public string object2 { get; set; }

	[JsonProperty("customer")]
	public int Customer { get; set; }

	[JsonProperty("invoice")]
	public int Invoice { get; set; }

	[JsonProperty("credit_note")]
	public object CreditNote { get; set; }

	[JsonProperty("date")]
	public int Date { get; set; }

	[JsonProperty("type")]
	public string Type { get; set; }

	[JsonProperty("method")]
	public string Method { get; set; }

	[JsonProperty("status")]
	public string Status { get; set; }

	[JsonProperty("gateway")]
	public object Gateway { get; set; }

	[JsonProperty("gateway_id")]
	public object GatewayId { get; set; }

	[JsonProperty("payment_source")]
	public object PaymentSource { get; set; }

	[JsonProperty("currency")]
	public string Currency { get; set; }

	[JsonProperty("amount")]
	public int Amount { get; set; }

	[JsonProperty("notes")]
	public object Notes { get; set; }

	[JsonProperty("parent_transaction")]
	public object ParentTransaction { get; set; }

	[JsonProperty("pdf_url")]
	public string PdfUrl { get; set; }

	[JsonProperty("created_at")]
	public int CreatedAt { get; set; }

	[JsonProperty("metadata")]
	public Metadata Metadata { get; set; }
}

}
