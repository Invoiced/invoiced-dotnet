using Newtonsoft.Json;

namespace Invoiced
{
    public class SubscriptionPreview : AbstractItem
    {
        [JsonProperty("first_invoice")] public Invoice FirstInvoice { get; set; }

        [JsonProperty("mrr")] public double? Mrr { get; set; }

        [JsonProperty("recurring_total")] public double? RecurringTotal { get; set; }

        protected override string EntityId()
        {
            return "SubscriptionPreview";
            // this is only used for json heading in ToString()
        }
    }
}