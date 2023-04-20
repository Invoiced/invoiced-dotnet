using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
    public class PaymentPlan : AbstractEntity<PaymentPlan>
    {
        public PaymentPlan(Connection conn) : base(conn)
        {
            EntityName = "/payment_plan";
        }

        public PaymentPlan()
        {
            EntityName = "/payment_plan";
        }

        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("installments")] public IList<PaymentPlanInstallment> Installments { get; set; }

        [JsonProperty("approval")] public Approval Approval { get; set; }

        [JsonProperty("created_at")] public long? CreatedAt { get; set; }

        [JsonProperty("updated_at")] public long? UpdatedAt { get; set; }

        protected override string EntityId()
        {
            return Id.ToString();
        }

        protected override bool HasList()
        {
            return false;
        }

        // identical to default Delete() but does not append ID to end of URL
        public void Cancel()
        {
            GetConnection().Delete(GetEndpoint(false));
        }
        public System.Threading.Tasks.Task CancelAsync()
        {
            return GetConnection().DeleteAsync(GetEndpoint(false));
        }

        // necessary to override this to avoid appending payment plan ID to DELETE request url
        public override void Delete()
        {
            Cancel();
        }
        public override System.Threading.Tasks.Task DeleteAsync()
        {
            return CancelAsync();
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

        public bool ShouldSerializeInstallments()
        {
            return CurrentOperation == "Create";
        }

        public bool ShouldSerializeApproval()
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