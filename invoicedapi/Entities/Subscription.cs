
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
public class Subscription :Entity<Subscription>
{

	internal Subscription(Connection conn) : base(conn) {
	}

	override public long getEntityID() {
		return this.id;
	}

	override public string getEntityName() {
		return "estimates";
	}

	override public bool hasCRUD() {
		return true;

	}

	override public bool hasList() {
		return false;
	}

	[JsonProperty("id")]
	public int id { get; set; }

	[JsonProperty("object")]
	public string object2 { get; set; }

	[JsonProperty("customer")]
	public int customer { get; set; }

	[JsonProperty("plan")]
	public string plan { get; set; }

	[JsonProperty("cycles")]
	public object cycles { get; set; }

	[JsonProperty("quantity")]
	public int quantity { get; set; }

	[JsonProperty("start_date")]
	public int start_date { get; set; }

	[JsonProperty("period_start")]
	public int period_start { get; set; }

	[JsonProperty("period_end")]
	public int period_end { get; set; }

	[JsonProperty("cancel_at_period_end")]
	public bool cancel_at_period_end { get; set; }

	[JsonProperty("canceled_at")]
	public object canceled_at { get; set; }

	[JsonProperty("status")]
	public string status { get; set; }

	[JsonProperty("addons")]
	public IList<Addon> addons { get; set; }

	[JsonProperty("discounts")]
	public IList<object> discounts { get; set; }

	[JsonProperty("taxes")]
	public IList<object> taxes { get; set; }

	[JsonProperty("url")]
	public string url { get; set; }

	[JsonProperty("created_at")]
	public int created_at { get; set; }

	[JsonProperty("metadata")]
	public Metadata metadata { get; set; }
}


public class Addon
{

	[JsonProperty("id")]
	public int id { get; set; }

	[JsonProperty("object")]
	public string object2 { get; set; }

	[JsonProperty("plan")]
	public string plan { get; set; }

	[JsonProperty("quantity")]
	public int quantity { get; set; }

	[JsonProperty("description")]
	public string description { get; set; }

	[JsonProperty("created_at")]
	public int created_at { get; set; }
}

}
