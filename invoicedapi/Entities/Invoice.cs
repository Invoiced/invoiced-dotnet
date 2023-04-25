using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
    public class Invoice : AbstractDocument<Invoice>
    {
        public Invoice(Connection conn) : base(conn)
        {
            EntityName = "/invoices";
        }

        public Invoice()
        {
            EntityName = "/invoices";
        }

        [JsonProperty("due_date")] public long? DueDate { get; set; }

        [JsonProperty("payment_terms")] public string PaymentTerms { get; set; }

        [JsonProperty("autopay")] public bool? Autopay { get; set; }

        [JsonProperty("paid")] public bool? Paid { get; set; }

        [JsonProperty("attempt_count")] public long? AttemptCount { get; set; }

        [JsonProperty("next_payment_attempt")] public long? NextPaymentAttempt { get; set; }

        [JsonProperty("subscription")] public long? Subscription { get; set; }

        [JsonProperty("payment_plan")] public long? PaymentPlan { get; set; }

        [JsonProperty("balance")] public double? Balance { get; set; }

        [JsonProperty("payment_url")] public string PaymentUrl { get; set; }

        [JsonProperty("ship_to")] public ShippingDetail ShipTo { get; set; }

        [JsonProperty("disabled_payment_methods")]
        public IList<string> DisabledPaymentMethods { get; set; }

        public PaymentPlan NewPaymentPlan()
        {
            var paymentPlan = new PaymentPlan(GetConnection());
            paymentPlan.SetEndpointBase(GetEndpoint(true));
            return paymentPlan;
        }

        public Note NewNote()
        {
            var note = new Note(GetConnection());
            note.SetEndpointBase(GetEndpoint(true));
            return note;
        }

        public void Pay()
        {
            AsyncUtil.RunSync(() => PayAsync());
        }
        public async System.Threading.Tasks.Task PayAsync()
        {
            var url = GetEndpoint(true) + "/pay";

            var responseText = await GetConnection().PostAsync(url, null, "");

            try
            {
                JsonConvert.PopulateObject(responseText, this);
            }
            catch (Exception e)
            {
                throw new EntityException("", e);
            }
        }

        // Conditional Serialisation

        public bool ShouldSerializePaid()
        {
            return false;
        }

        public bool ShouldSerializeAttemptCount()
        {
            return false;
        }

        public bool ShouldSerializeNextPaymentAttempt()
        {
            return false;
        }

        public bool ShouldSerializeSubscription()
        {
            return false;
        }

        public bool ShouldSerializeBalance()
        {
            return false;
        }

        public bool ShouldSerializePaymentPlan()
        {
            return false;
        }

        public bool ShouldSerializePaymentUrl()
        {
            return false;
        }
    }
}