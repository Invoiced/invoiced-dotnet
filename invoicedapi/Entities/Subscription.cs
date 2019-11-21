
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	// subscription has additional serialisation case 'Preview' in addition to the standard 'Create' and 'SaveAll' methods
	public class Subscription :AbstractEntity<Subscription>
	{

		internal Subscription(Connection conn) : base(conn) {
		}

		public Subscription() : base() {

		}

		protected override string EntityIdString() {
			return this.Id.ToString();
		}

		public override string EntityName() {
			return "subscriptions";
		}

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("object")]
		public string Obj { get; set; }

		[JsonProperty("customer")]
		public long Customer { get; set; }

		[JsonProperty("plan")]
		public string Plan { get; set; }

		[JsonProperty("cycles")]
		public long Cycles { get; set; }

		[JsonProperty("quantity")]
		public long Quantity { get; set; }

		[JsonProperty("start_date")]
		public long StartDate { get; set; }

		[JsonProperty("bill_in")]
		public string BillIn { get; set; }

		[JsonProperty("period_start")]
		public long PeriodStart { get; set; }

		[JsonProperty("period_end")]
		public long PeriodEnd { get; set; }

		[JsonProperty("cancel_at_period_end")]
		public bool CancelAtPeriodEnd { get; set; }

		[JsonProperty("canceled_at")]
		public object CanceledAt { get; set; }

		[JsonProperty("paused")]
		public bool Paused { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("contract_period_start")]
		public long? ContractPeriodStart { get; set; }

		[JsonProperty("contract_period_end")]
		public long? ContractPeriodEnd { get; set; }

		[JsonProperty("contract_renewal_cycles")]
		public long? ContractRenewalCycles { get; set; }

		[JsonProperty("contract_renewal_mode")]
		public string ContractRenewalMode { get; set; }

		[JsonProperty("addons")]
		public IList<SubscriptionAddon> Addons { get; set; }

		[JsonProperty("discounts")]
		public IList<object> Discounts { get; set; }

		[JsonProperty("taxes")]
		public IList<object> Taxes { get; set; }
		
		[JsonProperty("recurring_total")]
		public long RecurringTotal { get; set; }

		[JsonProperty("mrr")]
		public long Mrr { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("created_at")]
		public long CreatedAt { get; set; }

		[JsonProperty("metadata")]
		public Metadata Metadata { get; set; }

		[JsonProperty("pending_line_items")]
		public IList<string> PendingLineItems { get; set; }

		public void Cancel() {
			string url = this.Connection.baseUrl() + "/" + this.EntityName() + "/" + this.EntityIdString();
			
			this.Connection.Delete(url);
		}

		public SubscriptionPreview Preview() {

			string url = this.Connection.baseUrl() + "/" + this.EntityName() + "/preview";

			string jsonRequestBody = this.ToJsonString();

			string responseText = this.Connection.Post(url,null,jsonRequestBody);
			SubscriptionPreview serializedObject;
			
			try {
					serializedObject = JsonConvert.DeserializeObject<SubscriptionPreview>(responseText,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
			} catch(Exception e) {
				throw new EntityException("",e);
			}

			return serializedObject;

		}

		// Conditional Serialisation

		public bool ShouldSerializeId() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeObj() {
			return false;
		}

		public bool ShouldSerializeCustomer() {
			return this.CurrentOperation != "SaveAll";
		}

		public bool ShouldSerializePlan() {
			return this.CurrentOperation != "SaveAll";
		}

		public bool ShouldSerializeCycles() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeStartDate() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeBillIn() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializePeriodStart() {
			return false;
		}

		public bool ShouldSerializePeriodEnd() {
			return false;
		}

		public bool ShouldSerializeCancelAtPeriodEnd() {
			return this.CurrentOperation != "Preview";
		}

		public bool ShouldSerializeCanceledAt() {
			return false;
		}

		public bool ShouldSerializePaused() {
			return this.CurrentOperation != "Preview";
		}

		public bool ShouldSerializeStatus() {
			return false;
		}

		public bool ShouldSerializeContractPeriodStart() {
			return false;
		}

		public bool ShouldSerializeContractPeriodEnd() {
			return false;
		}

		public bool ShouldSerializeRenewalCycles() {
			return this.CurrentOperation != "Preview";
		}

		public bool ShouldSerializeRenewalMode() {
			return this.CurrentOperation != "Preview";
		}

		public bool ShouldSerializeRecurringTotal() {
			return false;
		}

		public bool ShouldSerializeMrr() {
			return false;
		}

		public bool ShouldSerializeUrl() {
			return false;
		}

		public bool ShouldSerializeCreatedAt() {
			return false;
		}

		public bool ShouldSerializeMetadata() {
			return this.CurrentOperation != "Preview";
		}

		public bool ShouldSerializePendingLineItems() {
			return this.CurrentOperation == "Preview";
		}
	
	}
}
