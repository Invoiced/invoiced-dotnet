using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
    public class Payment : AbstractEntity<Payment>
    {
        internal Payment(Connection conn) : base(conn)
        {
            EntityName = "/payments";
        }

        public Payment()
        {
            EntityName = "/payments";
        }

        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("customer")] public long? Customer { get; set; }

        [JsonProperty("date")] public long? Date { get; set; }

        [JsonProperty("method")] public string Method { get; set; }

        [JsonProperty("matched")] public bool? Matched { get; set; }

        [JsonProperty("voided")] public bool? Voided { get; set; }

        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("currency")] public string Currency { get; set; }

        [JsonProperty("amount")] public double? Amount { get; set; }

        [JsonProperty("balance")] public double? Balance { get; set; }

        [JsonProperty("reference")] public string Reference { get; set; }

        [JsonProperty("source")] public string Source { get; set; }

        [JsonProperty("notes")] public string Notes { get; set; }

        [JsonProperty("pdf_url")] public string PdfUrl { get; set; }

        [JsonProperty("charge")] public Charge Charge { get; set; }

        [JsonProperty("applied_to")] public IList<PaymentItem> AppliedTo { get; set; }

        [JsonProperty("created_at")] public long? CreatedAt { get; set; }

        [JsonProperty("updated_at")] public long? UpdatedAt { get; set; }

        protected override string EntityId()
        {
            return Id.ToString();
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

        public bool ShouldSerializeStatus()
        {
            return false;
        }

        public bool ShouldSerializeBalance()
        {
            return false;
        }

        public bool ShouldSerializeMatched()
        {
            return false;
        }

        public bool ShouldSerializeVoided()
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