using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
    public class Customer : AbstractEntity<Customer>
    {
        internal Customer(Connection conn) : base(conn)
        {
            EntityName = "/customers";
        }

        public Customer()
        {
            EntityName = "/customers";
        }

        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("number")] public string Number { get; set; }

        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("autopay")] public bool? Autopay { get; set; }

        [JsonProperty("autopay_delay_days")] public long? AutopayDelayDays { get; set; }

        [JsonProperty("payment_terms")] public string PaymentTerms { get; set; }

        [JsonProperty("payment_source")] public PaymentSource PaymentSource { get; set; }

        [JsonProperty("disabled_payment_methods")]
        public IList<string> DisabledPaymentMethods { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("attention_to")] public string AttentionTo { get; set; }

        [JsonProperty("address1")] public string Address1 { get; set; }

        [JsonProperty("address2")] public string Address2 { get; set; }

        [JsonProperty("city")] public string City { get; set; }

        [JsonProperty("state")] public string State { get; set; }

        [JsonProperty("postal_code")] public string PostalCode { get; set; }

        [JsonProperty("country")] public string Country { get; set; }

        [JsonProperty("language")] public string Language { get; set; }

        [JsonProperty("chase")] public bool? Chase { get; set; }

        [JsonProperty("chasing_cadence")] public long? ChasingCadence { get; set; }

        [JsonProperty("next_chase_step")] public long? NextChaseStep { get; set; }

        [JsonProperty("taxable")] public bool? Taxable { get; set; }

        [JsonProperty("tax_id")] public string TaxId { get; set; }

        [JsonProperty("taxes")] public IList<Tax> Taxes { get; set; }

        [JsonProperty("avalara_entity_use_code")]
        public string AvalaraEntityUseCode { get; set; }

        [JsonProperty("avalara_exemption_number")]
        public string AvalaraExemptionNumber { get; set; }

        [JsonProperty("phone")] public string Phone { get; set; }

        [JsonProperty("credit_hold")] public bool? CreditHold { get; set; }

        [JsonProperty("credit_limit")] public double? CreditLimit { get; set; }

        [JsonProperty("owner")] public long? Owner { get; set; }

        [JsonProperty("notes")] public string Notes { get; set; }

        [JsonProperty("parent_customer")] public long? ParentCustomer { get; set; }

        [JsonProperty("bill_to_parent")] public bool? BillToParent { get; set; }

        [JsonProperty("sign_up_page")] public string SignUpPage { get; set; }

        [JsonProperty("sign_up_url")] public string SignUpUrl { get; set; }

        [JsonProperty("statement_pdf_url")] public string StatementPdfUrl { get; set; }

        [JsonProperty("created_at")] public long? CreatedAt { get; set; }

        [JsonProperty("updated_at")] public long? UpdatedAt { get; set; }

        [JsonProperty("metadata")] public Metadata Metadata { get; set; }

        protected override string EntityId()
        {
            return Id.ToString();
        }

        protected override bool HasSends()
        {
            return true;
        }

        public Note NewNote()
        {
            var note = new Note(GetConnection());
            note.SetEndpointBase(GetEndpoint(true));
            note.Customer = Id;
            return note;
        }

        public Contact NewContact()
        {
            var contact = new Contact(GetConnection());
            contact.SetEndpointBase(GetEndpoint(true));
            return contact;
        }

        public PendingLineItem NewPendingLineItem()
        {
            var pli = new PendingLineItem(GetConnection());
            pli.SetEndpointBase(GetEndpoint(true));
            return pli;
        }

        public Task NewTask()
        {
            var task = new Task(GetConnection());
            task.CustomerId = Id;
            return task;
        }

        public IList<Email> SendStatementEmail(EmailRequest emailRequest)
        {
            return SendEmail(emailRequest);
        }

        public Letter SendStatementLetter(LetterRequest letterRequest = null)
        {
            return SendLetter(letterRequest);
        }

        public IList<TextMessage> SendStatementText(TextRequest textRequest)
        {
            return SendText(textRequest);
        }

        public Balance GetBalance()
        {
            var url = GetEndpoint(true) + "/balance";

            var responseText = GetConnection().Get(url, null);

            try
            {
                return JsonConvert.DeserializeObject<Balance>(responseText,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore
                    });
            }
            catch (Exception e)
            {
                throw new EntityException("", e);
            }
        }

        public Invoice ConsolidateInvoices(long? cutoffDate = null)
        {
            var url = GetEndpoint(true) + "/consolidate_invoices";

            var responseText = GetConnection().Post(url, null, cutoffDate.ToString());
            Invoice serializedObject;

            try
            {
                serializedObject = JsonConvert.DeserializeObject<Invoice>(responseText,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore
                    });
                serializedObject.ChangeConnection(GetConnection());
            }
            catch (Exception e)
            {
                throw new EntityException("", e);
            }

            return serializedObject;
        }

        public PaymentSource CreatePaymentSource(SourceRequest sourceRequest)
        {
            var url = GetEndpoint(true) + "/payment_sources";
            PaymentSource output = null;

            try
            {
                var sourceRequestJson = sourceRequest.ToJsonString();
                var response = GetConnection().Post(url, null, sourceRequestJson);

                var sourceSettings = new JsonSerializerSettings
                    {NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore};
                sourceSettings.Converters.Add(new PaymentSourceConverter());

                output = JsonConvert.DeserializeObject<PaymentSource>(response, sourceSettings);
                output.ChangeConnection(GetConnection());
                output.SetEndpointBase(GetEndpoint(true));
            }
            catch (Exception e)
            {
                throw new EntityException("", e);
            }

            return output;
        }

        public EntityList<PaymentSource> ListPaymentSources()
        {
            var source = new PaymentSource(GetConnection());
            source.SetEndpointBase(GetEndpoint(true));
            return source.ListAll(null, null, new PaymentSourceConverter());
        }

        // Conditional Serialisation
        public bool ShouldSerializeId()
        {
            return false;
        }

        public bool ShouldSerializeObject()
        {
            return false;
        }

        public bool ShouldSerializeNextChaseStep()
        {
            return false;
        }

        public bool ShouldSerializeAutopayDelayDays()
        {
            return CurrentOperation == "Create";
        }

        public bool ShouldSerializeSignUpUrl()
        {
            return false;
        }

        public bool ShouldSerializeStatementPdfUrl()
        {
            return false;
        }

        public bool ShouldSerializeCreatedAt()
        {
            return false;
        }

        public bool ShouldSerializeUpdatedAt()
        {
            return false;
        }
    }
}