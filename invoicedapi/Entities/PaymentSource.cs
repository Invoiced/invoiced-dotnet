using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class PaymentSource : AbstractEntity<PaymentSource>
	{
		
		public PaymentSource() : base() {
			this.EntityName = "/payment_sources";
		}
		
		internal PaymentSource(Connection conn) : base(conn) {
			this.EntityName = "/payment_sources";
		}

		protected override bool HasCrud() {
			return false;
		}

		[JsonProperty("id")]
		public long? Id { get; set; }

		[JsonProperty("object")]
		public string Obj { get; set; }

		protected override string EntityId() {
			return this.Id.ToString();
		}

	}

}
