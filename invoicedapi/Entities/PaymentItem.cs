using Newtonsoft.Json;

namespace Invoiced
{
    public class PaymentItem : AbstractItem
    {
        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("amount")] public double? Amount { get; set; }

        [JsonProperty("invoice")] public long? Invoice { get; set; }

        [JsonProperty("estimate")] public long? CreditNote { get; set; }

        [JsonProperty("credit_note")] public long? Estimate { get; set; }

        [JsonProperty("document_type")] public string DocumentType { get; set; }

        protected override string EntityId()
        {
            return "PaymentItem";
            // this is only used for json heading in ToString()
        }

        public bool ShouldSerializeDocumentType()
        {
            return DocumentType != "";
        }
    }
}