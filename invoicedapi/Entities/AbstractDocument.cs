using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
    public abstract class AbstractDocument<T> : AbstractEntity<T> where T : AbstractEntity<T>
    {
        public AbstractDocument(Connection conn) : base(conn)
        {
        }

        public AbstractDocument()
        {
        }

        // numerous properties must be nullable due to avoid errors with subscription preview `first_invoice` objects
        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("customer")] public long? Customer { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("number")] public string Number { get; set; }

        [JsonProperty("currency")] public string Currency { get; set; }

        [JsonProperty("draft")] public bool? Draft { get; set; }

        [JsonProperty("closed")] public bool? Closed { get; set; }

        [JsonProperty("voided")] public bool? Voided { get; set; }

        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("date")] public long? Date { get; set; }

        [JsonProperty("purchase_order")] public string PurchaseOrder { get; set; }

        [JsonProperty("items")] public IList<LineItem> Items { get; set; }

        [JsonProperty("notes")] public string Notes { get; set; }

        [JsonProperty("subtotal")] public double? Subtotal { get; set; }

        [JsonProperty("discounts")] public IList<Discount> Discounts { get; set; }

        [JsonProperty("taxes")] public IList<Tax> Taxes { get; set; }

        [JsonProperty("total")] public double? Total { get; set; }

        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("pdf_url")] public string PdfUrl { get; set; }

        [JsonProperty("created_at")] public long? CreatedAt { get; set; }

        [JsonProperty("metadata")] public Metadata Metadata { get; set; }

        [JsonProperty("attachments")] public IList<long> Attachments { get; set; }

        [JsonProperty("calculate_taxes")] public bool? CalculateTaxes { get; set; }

        [JsonProperty("updated_at")] public long? UpdatedAt { get; set; }

        protected override string EntityId()
        {
            return Id.ToString();
        }

        protected override bool HasVoid()
        {
            return true;
        }

        protected override bool HasAttachments()
        {
            return true;
        }

        protected override bool HasSends()
        {
            return true;
        }

        public bool ShouldSerializeId()
        {
            return false;
        }

        public bool ShouldSerializeObject()
        {
            return false;
        }

        public bool ShouldSerializeCustomer()
        {
            return CurrentOperation == nameof(Create) || CurrentOperation == nameof(CreateAsync);
        }

        public bool ShouldSerializeStatus()
        {
            return false;
        }

        public bool ShouldSerializeSubtotal()
        {
            return false;
        }

        public bool ShouldSerializeTotal()
        {
            return false;
        }

        public bool ShouldSerializeUrl()
        {
            return false;
        }

        public bool ShouldSerializePdfUrl()
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