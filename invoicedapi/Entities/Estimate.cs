using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Estimate : AbstractEntity<Estimate>
	{

		public Estimate() : base(){

		}

		internal Estimate(Connection conn) : base(conn) {
		}

		public override long EntityId() {
			return this.Id;
		}

		public override string EntityIdString() {
			return this.Id.ToString();
		}

		public override string EntityName() {
			return "estimates";
		}

		public override bool HasVoid() {
			return true;
		}

		public override bool HasAttachments() {
			return true;
		}

		public override bool HasSends() {
			return true;
		}

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("object")]
		public string Obj { get; set; }

		[JsonProperty("customer")]
		public long Customer { get; set; }

		[JsonProperty("invoice")]
		public long Invoice { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("draft")]
		public bool Draft { get; set; }

		[JsonProperty("closed")]
		public bool Closed { get; set; }

		[JsonProperty("approved")]
		public bool Approved { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("number")]
		public string Number { get; set; }

		[JsonProperty("date")]
		public long Date { get; set; }

		[JsonProperty("expiration_date")]
		public long ExpirationDate { get; set; }

		[JsonProperty("payment_terms")]
		public string PaymentTerms { get; set; }

		[JsonProperty("items")]
		public IList<LineItem> Items { get; set; }

		[JsonProperty("notes")]
		public string Notes { get; set; }

		[JsonProperty("subtotal")]
		public long Subtotal { get; set; }

		[JsonProperty("discounts")]
		public IList<Discount> Discounts { get; set; }

		[JsonProperty("taxes")]
		public IList<Tax> Taxes { get; set; }

		[JsonProperty("total")]
		public long Total { get; set; }

		[JsonProperty("deposit")]
		public long Deposit { get; set; }

		[JsonProperty("deposit_paid")]
		public bool DepositPaid { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("pdf_url")]
		public string PdfUrl { get; set; }

		[JsonProperty("created_at")]
		public long CreatedAt { get; set; }

		[JsonProperty("metadata")]
		public Metadata Metadata { get; set; }

		[JsonProperty("calculate_taxes")]
		public bool CalculateTaxes { get; set; }

		[JsonProperty("disabled_payment_methods")]
		public IList<string> DisabledPaymentMethods { get; set; }

		public Invoice ConvertToInvoice() {

			string url = this.connection.baseUrl() + "/" + this.EntityName() + "/" + this.EntityIdString() + "/invoice";

			string responseText = this.connection.Post(url,null,"");
			Invoice serializedObject;
			
			try {
					serializedObject = JsonConvert.DeserializeObject<Invoice>(responseText,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
					serializedObject.ChangeConnection(this.GetConnection());
			} catch(Exception e) {
				throw new EntityException("",e);
			}

			return serializedObject;

		}

		// Conditional Serialisation
		
		public bool ShouldSerializeId() {
			return false;
		}

		public bool ShouldSerializeObj() {
			return false;
		}

		public bool ShouldSerializeCustomer() {
			if (this.currentOperation != "Create") return false;
			return true;
		}

		public bool ShouldSerializeInvoice() {
			if (this.currentOperation != "Create") return false;
			return true;
		}

		public bool ShouldSerializeApproved() {
			return false;
		}

		public bool ShouldSerializeStatus() {
			return false;
		}

		public bool ShouldSerializeSubtotal() {
			return false;
		}

		public bool ShouldSerializeTotal() {
			return false;
		}

		public bool ShouldSerializeUrl() {
			return false;
		}

		public bool ShouldSerializePdfUrl() {
			return false;
		}

		public bool ShouldSerializeCreatedAt() {
			return false;
		}

	}

}
