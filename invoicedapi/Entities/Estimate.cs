using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
	public class Estimate : AbstractDocument<Estimate>
	{
		public Estimate() : base() {
			this.EntityName = "/estimates";
		}

		internal Estimate(Connection conn) : base(conn) {
			this.EntityName = "/estimates";
		}

		[JsonProperty("invoice")]
		public long? Invoice { get; set; }

		[JsonProperty("approved")]
		public bool? Approved { get; set; }

		[JsonProperty("expiration_date")]
		public long? ExpirationDate { get; set; }

		[JsonProperty("payment_terms")]
		public string PaymentTerms { get; set; }

		[JsonProperty("deposit")]
		public double? Deposit { get; set; }

		[JsonProperty("deposit_paid")]
		public bool? DepositPaid { get; set; }

		[JsonProperty("ship_to")]
		public object ShipTo { get; set; }

		public Invoice ConvertToInvoice() {
			string url = this.GetEndpoint(true) + "/invoice";

			string responseText = this.GetConnection().Post(url,null,"");
			Invoice serializedObject;
			
			try {
					serializedObject = JsonConvert.DeserializeObject<Invoice>(responseText,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
					serializedObject.ChangeConnection(this.GetConnection());
			} catch(Exception e) {
				throw new EntityException("",e);
			}

			return serializedObject;
		}

		// Conditional Serialisation
		public bool ShouldSerializeInvoice() {
			if (this.CurrentOperation != "Create") return false;
			return true;
		}

		public bool ShouldSerializeApproved() {
			return false;
		}
	}
}
