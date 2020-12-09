using Newtonsoft.Json;

namespace Invoiced
{
    public class PaymentPlanInstallment : AbstractItem
    {
        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("date")] public long? Date { get; set; }

        [JsonProperty("amount")] public double? Amount { get; set; }

        [JsonProperty("balance")] public double? Balance { get; set; }

        protected override string EntityId()
        {
            return Id.ToString();
        }
    }
}