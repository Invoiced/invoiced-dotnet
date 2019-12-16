using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Card : PaymentSource
	{
		
		public Card() : base() {
			this.EntityName = "/cards";
		}
		
		internal Card(Connection conn) : base(conn) {
			this.EntityName = "/cards";
		}

		[JsonProperty("brand")]
		public string Brand { get; set; }

		[JsonProperty("last4")]
		public string Last4 { get; set; }
		
		[JsonProperty("exp_month")]
		public int? ExpMonth { get; set; }
		
		[JsonProperty("exp_year")]
		public int? ExpYear { get; set; }
		
		[JsonProperty("funding")]
		public string Funding { get; set; }

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
