using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
    public class Balance : AbstractItem
    {
        [JsonProperty("currency")] public string Currency { get; set; }

        [JsonProperty("available_credits")] public double? AvailableCredits { get; set; }

        [JsonProperty("history")] public IList<BalanceHistory> History { get; set; }

        [JsonProperty("past_due")] public bool? PastDue { get; set; }

        [JsonProperty("total_outstanding")] public double? TotalOutstanding { get; set; }

        [JsonProperty("due_now")] public double? DueNow { get; set; }

        protected override string EntityId()
        {
            return "Balance";
            // this is only used for json heading in ToString()
        }
    }
}