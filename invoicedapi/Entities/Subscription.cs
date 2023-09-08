using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Invoiced
{
    // subscription has additional serialisation case 'Preview' in addition to the standard 'Create' and 'SaveAll' methods
    public class Subscription : AbstractEntity<Subscription>
    {
        internal Subscription(Connection conn) : base(conn)
        {
            EntityName = "/subscriptions";
        }

        public Subscription()
        {
            EntityName = "/subscriptions";
        }

        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("customer")] public long? Customer { get; set; }

        [JsonProperty("amount")] public double? Amount { get; set; }

        [JsonProperty("plan")] public string Plan { get; set; }

        [JsonProperty("start_date")] public long? StartDate { get; set; }

        [JsonProperty("bill_in")] public string BillIn { get; set; }

        [JsonProperty("bill_in_advance_days")] public long? BillInAdvanceDays { get; set; }

        [JsonProperty("quantity")] public long? Quantity { get; set; }

        [JsonProperty("cycles")] public long? Cycles { get; set; }

        [JsonProperty("period_start")] public long? PeriodStart { get; set; }

        [JsonProperty("period_end")] public long? PeriodEnd { get; set; }

        [JsonProperty("snap_to_nth_day")] public long? SnapToNthDay { get; set; }

        [JsonProperty("cancel_at_period_end")] public bool? CancelAtPeriodEnd { get; set; }

        [JsonProperty("canceled_at")] public object CanceledAt { get; set; }

        [JsonProperty("prorate")] public bool? Prorate { get; set; }

        [JsonProperty("proration_date")] public long? ProrationDate { get; set; }

        [JsonProperty("paused")] public bool? Paused { get; set; }

        [JsonProperty("contract_period_start")]
        public long? ContractPeriodStart { get; set; }

        [JsonProperty("contract_period_end")] public long? ContractPeriodEnd { get; set; }

        [JsonProperty("contract_renewal_cycles")]
        public long? ContractRenewalCycles { get; set; }

        [JsonProperty("contract_renewal_mode")]
        public string ContractRenewalMode { get; set; }

        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("recurring_total")] public double? RecurringTotal { get; set; }

        [JsonProperty("mrr")] public double? Mrr { get; set; }

        [JsonProperty("addons")] public IList<SubscriptionAddon> Addons { get; set; }

        [JsonProperty("discounts")] public IList<object> Discounts { get; set; }

        [JsonProperty("taxes")] public IList<object> Taxes { get; set; }

        [JsonProperty("ship_to")] public ShippingDetail ShipTo { get; set; }

        [JsonProperty("pending_line_items")] public IList<string> PendingLineItems { get; set; }

        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("created_at")] public long? CreatedAt { get; set; }

        [JsonProperty("updated_at")] public long? UpdatedAt { get; set; }

        [JsonProperty("metadata")] public Metadata Metadata { get; set; }

        protected override string EntityId()
        {
            return Id.ToString();
        }

        public void Cancel()
        {
            AsyncUtil.RunSync(() => CancelAsync());
        }
        public System.Threading.Tasks.Task CancelAsync()
        {
            return GetConnection().DeleteAsync(GetEndpoint(true));
        }

        public SubscriptionPreview Preview()
        {
            return AsyncUtil.RunSync(() => PreviewAsync());
        }
        public async Task<SubscriptionPreview> PreviewAsync()
        {
            var responseText = await GetConnection().PostAsync("/subscriptions/preview", null, ToJsonString());

            try
            {
                return JsonConvert.DeserializeObject<SubscriptionPreview>(responseText,
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

        // Conditional Serialisation

        public bool ShouldSerializeId()
        {
            return CurrentOperation == nameof(Create) || CurrentOperation == nameof(CreateAsync);
        }

        public bool ShouldSerializeObject()
        {
            return false;
        }

        public bool ShouldSerializeCustomer()
        {
            return CurrentOperation != nameof(SaveAll) && CurrentOperation != nameof(SaveAllAsync);
        }

        public bool ShouldSerializePlan()
        {
            return CurrentOperation != nameof(SaveAll) && CurrentOperation != nameof(SaveAllAsync);
        }

        public bool ShouldSerializeCycles()
        {
            return CurrentOperation == nameof(Create) || CurrentOperation == nameof(CreateAsync);
        }

        public bool ShouldSerializeStartDate()
        {
            return CurrentOperation == nameof(Create) || CurrentOperation == nameof(CreateAsync);
        }

        public bool ShouldSerializeBillIn()
        {
            return CurrentOperation == nameof(Create) || CurrentOperation == nameof(CreateAsync);
        }

        public bool ShouldSerializePeriodStart()
        {
            return false;
        }

        public bool ShouldSerializePeriodEnd()
        {
            return false;
        }

        public bool ShouldSerializeCancelAtPeriodEnd()
        {
            return CurrentOperation != nameof(Preview) && CurrentOperation != nameof(PreviewAsync);
        }

        public bool ShouldSerializeCanceledAt()
        {
            return false;
        }

        public bool ShouldSerializePaused()
        {
            return CurrentOperation != nameof(Preview) && CurrentOperation != nameof(PreviewAsync);
        }

        public bool ShouldSerializeStatus()
        {
            return false;
        }

        public bool ShouldSerializeContractPeriodStart()
        {
            return false;
        }

        public bool ShouldSerializeContractPeriodEnd()
        {
            return false;
        }

        public bool ShouldSerializeRenewalCycles()
        {
            return CurrentOperation != nameof(Preview) && CurrentOperation != nameof(PreviewAsync);
        }

        public bool ShouldSerializeRenewalMode()
        {
            return CurrentOperation != nameof(Preview) && CurrentOperation != nameof(PreviewAsync);
        }

        public bool ShouldSerializeRecurringTotal()
        {
            return false;
        }

        public bool ShouldSerializeMrr()
        {
            return false;
        }

        public bool ShouldSerializeUrl()
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

        public bool ShouldSerializeMetadata()
        {
            return CurrentOperation != nameof(Preview) && CurrentOperation != nameof(PreviewAsync);
        }

        public bool ShouldSerializePendingLineItems()
        {
            return CurrentOperation == nameof(Preview) || CurrentOperation == nameof(PreviewAsync);
        }
    }
}