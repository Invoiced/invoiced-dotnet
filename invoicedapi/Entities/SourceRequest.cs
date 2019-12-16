using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class SourceRequest : AbstractItem
	{
		
		public SourceRequest() : base() {

		}

        [JsonProperty("method")]
		public string Method { get; set; }

        [JsonProperty("make_default")]
		public bool MakeDefault { get; set; }

        [JsonProperty("invoiced_token")]
		public string InvoicedToken { get; set; }
		
		[JsonProperty("gateway_token")]
		public string GatewayToken { get; set; }

		protected override string EntityId() {
			return "SourceRequest";
            // this is only used for json heading in ToString()
		}
		
	}

}
