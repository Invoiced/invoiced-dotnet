using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Customer : AbstractEntity<Customer>
	{
		internal Customer(Connection conn) : base(conn) {
			this.EntityName = "/customers";
		}

		public Customer() : base() {
			this.EntityName = "/customers";
		}

		protected override string EntityId() {
			return this.Id.ToString();
		}

		protected override bool HasSends() {
			return true;
		}

		[JsonProperty("id")]
		public long? Id { get; set; }

		[JsonProperty("object")]
		public string Obj { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("number")]
		public string Number { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("autopay")]
		public bool? Autopay { get; set; }

		[JsonProperty("autopay_delay_days")]
		public long? AutopayDelayDays { get; set; }

		[JsonProperty("payment_terms")]
		public string PaymentTerms { get; set; }

		[JsonProperty("payment_source")]
		public PaymentSource PaymentSource { get; set; }

		[JsonProperty("taxable")]
		public bool? Taxable { get; set; }

		[JsonProperty("taxes")]
		public IList<Tax> Taxes { get; set; }

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
		public bool? Chase { get; set; }

		[JsonProperty("chasing_cadence")]
		public long? ChasingCadence { get; set; }

		[JsonProperty("next_chase_step")]
		public long? NextChaseStep { get; set; }

		[JsonProperty("tax_id")]
		public string TaxId { get; set; }

		[JsonProperty("avalara_entity_use_code")]
		public string AvalaraEntityUseCode { get; set; }

		[JsonProperty("avalara_exemption_number")]
		public string AvalaraExemptionNumber { get; set; }

		[JsonProperty("phone")]
		public string Phone { get; set; }

		[JsonProperty("credit_hold")]
		public bool? CreditHold { get; set; }

		[JsonProperty("credit_limit")]
		public double? CreditLimit { get; set; }

		[JsonProperty("owner")]
		public long? Owner { get; set; }

		[JsonProperty("parent_customer")]
		public long? ParentCustomer { get; set; }

		[JsonProperty("notes")]
		public string Notes { get; set; }

		[JsonProperty("sign_up_page")]
		public string SignUpPage { get; set; }

		[JsonProperty("sign_up_url")]
		public string SignUpUrl { get; set; }

		[JsonProperty("statement_pdf_url")]
		public string StatementPdfUrl { get; set; }

		[JsonProperty("created_at")]
		public long? CreatedAt { get; set; }

		[JsonProperty("metadata")]
		public Metadata Metadata { get; set; }

		[JsonProperty("disabled_payment_methods")]
		public IList<string> DisabledPaymentMethods { get; set; }

		public Note NewNote() {
			Note note = new Note(this.GetConnection());
			note.SetEndpointBase(this.GetEndpoint(true));
			note.CustomerId = this.Id;
			return note;
		}

		public Contact NewContact() {
			Contact contact = new Contact(this.GetConnection());
			contact.SetEndpointBase(this.GetEndpoint(true));
			return contact;
		}

		public PendingLineItem NewPendingLineItem() {
			PendingLineItem pli = new PendingLineItem(this.GetConnection());
			pli.SetEndpointBase(this.GetEndpoint(true));
			return pli;
		}

		public Task NewTask() {
			Task task = new Task(this.GetConnection());
			task.CustomerId = this.Id;
			return task;
		}

		public IList<Email> SendStatementEmail(EmailRequest emailRequest) {
			return this.SendEmail(emailRequest);
		}

		public Letter SendStatementLetter(LetterRequest letterRequest = null) {
			return this.SendLetter(letterRequest);
		}

		public IList<TextMessage> SendStatementText(TextRequest textRequest) {
			return this.SendText(textRequest);
		}

		public Balance GetBalance() {

			var url = this.GetEndpoint(true) + "/balance";

			var responseText = this.GetConnection().Get(url,null);
			Balance serializedObject;
			
			try {
					serializedObject = JsonConvert.DeserializeObject<Balance>(responseText,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
			} catch(Exception e) {
				throw new EntityException("",e);
			}

			return serializedObject;

		}

		public Invoice ConsolidateInvoices(long? cutoffDate = null) {

			string url = this.GetEndpoint(true) + "/consolidate_invoices";

			string responseText = this.GetConnection().Post(url,null,cutoffDate.ToString());
			Invoice serializedObject;
			
			try {
					serializedObject = JsonConvert.DeserializeObject<Invoice>(responseText,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
					serializedObject.ChangeConnection(this.GetConnection());
			} catch(Exception e) {
				throw new EntityException("",e);
			}

			return serializedObject;

		}

		public PaymentSource CreatePaymentSource(SourceRequest sourceRequest) {
			string url = this.GetEndpoint(true) + "/payment_sources";
			PaymentSource output = null;

			try {

				string sourceRequestJson = sourceRequest.ToJsonString();
				string response = this.GetConnection().Post(url, null, sourceRequestJson);
				
				JsonSerializerSettings sourceSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore};
				sourceSettings.Converters.Add(new PaymentSourceConverter());
				
				output = JsonConvert.DeserializeObject<PaymentSource>(response, sourceSettings);
				output.ChangeConnection(this.GetConnection());
				output.SetEndpointBase(this.GetEndpoint(true));
			}
			catch (Exception e) {
				throw new EntityException("", e);
			}

			return output;
		}

		public EntityList<PaymentSource> ListPaymentSources() {
			PaymentSource source = new PaymentSource(this.GetConnection());
			source.SetEndpointBase(this.GetEndpoint(true));
			return source.ListAll(null, null, new PaymentSourceConverter());
		}

		// Conditional Serialisation

		public bool ShouldSerializeId() {
			return false;
		}

		public bool ShouldSerializeObj() {
			return false;
		}

		public bool ShouldSerializeNextChaseStep() {
			return false;
		}

		public bool ShouldSerializeAutopayDelayDays() {
			return CurrentOperation == "Create";
		}

		public bool ShouldSerializeSignUpUrl() {
			return false;
		}

		public bool ShouldSerializeStatementPdfUrl() {
			return false;
		}

		public bool ShouldSerializeCreatedAt() {
			return false;
		}

	}

}
