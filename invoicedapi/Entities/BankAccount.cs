using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class BankAccount : PaymentSource
	{
		
		public BankAccount() : base() {
			this.EntityName = "/bank_accounts";
		}
		
		internal BankAccount(Connection conn) : base(conn) {
			this.EntityName = "/bank_accounts";
		}

		[JsonProperty("bank_name")]
		public string BankName { get; set; }

		[JsonProperty("last4")]
		public string Last4 { get; set; }
		
		[JsonProperty("routing_number")]
		public string RoutingNumber { get; set; }
		
		[JsonProperty("verified")]
		public bool? Verified { get; set; }
		
		[JsonProperty("currency")]
		public string Currency { get; set; }

		public override void Delete()
		{
			try {
				this.GetConnection().Delete(this.GetEndpoint(true));
			}
			catch (Exception e) {
				throw new EntityException("",e);
			}
		}
	}

}
