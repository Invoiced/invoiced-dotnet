
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Subscription :Entity<Subscription>
	{

		internal Subscription(Connection conn) : base(conn) {
		}

		public Subscription() : base() {

		}

		public bool ShouldSerializeId() {
			return false;
		}

		override public long EntityID() {
			return this.Id;
		}

		override public string EntityIDString() {
			throw new EntityException(this.EntityName() + " ID type is long, not string");
		}

		override public string EntityName() {
			return "estimates";
		}

		override public bool HasCRUD() {
			return true;
		}

		override public bool HasList() {
			return true;
		}

		override public bool HasStringID() {
			return false;
		}

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("object")]
		public string Object2 { get; set; }

		[JsonProperty("customer")]
		public int Customer { get; set; }

		[JsonProperty("plan")]
		public string Plan { get; set; }

		[JsonProperty("cycles")]
		public object Cycles { get; set; }

		[JsonProperty("quantity")]
		public int Quantity { get; set; }

		[JsonProperty("start_date")]
		public int StartDate { get; set; }

		[JsonProperty("period_start")]
		public int PeriodStart { get; set; }

		[JsonProperty("period_end")]
		public int PeriodEnd { get; set; }

		[JsonProperty("cancel_at_period_end")]
		public bool CancelAtPeriodEnd { get; set; }

		[JsonProperty("canceled_at")]
		public object CanceledAt { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("addons")]
		public IList<Addon> Addons { get; set; }

		[JsonProperty("discounts")]
		public IList<object> Discounts { get; set; }

		[JsonProperty("taxes")]
		public IList<object> Taxes { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("created_at")]
		public long CreatedAt { get; set; }

		[JsonProperty("metadata")]
		public Metadata Metadata { get; set; }
	
	}

	public class Addon
	{

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("object")]
		public string Object2 { get; set; }

		[JsonProperty("plan")]
		public string Plan { get; set; }

		[JsonProperty("quantity")]
		public int Quantity { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("created_at")]
		public long CreatedAt { get; set; }

	}

}
