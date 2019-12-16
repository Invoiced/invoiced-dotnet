using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class ChargeRequest : AbstractItem
	{
		
		public ChargeRequest() : base() {

		}

		[JsonProperty("customer")]
		public long? Customer { get; set; }

		[JsonProperty("method")]
		public string Method { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("amount")]
		public long? Amount { get; set; }

		[JsonProperty("invoiced_token")]
		public string InvoicedToken { get; set; }

		[JsonProperty("gateway_token")]
		public string GatewayToken { get; set; }

		[JsonProperty("payment_source_type")]
		public string PaymentSourceType { get; set; }

		[JsonProperty("payment_source_id")]
		public long? PaymentSourceId { get; set; }

		[JsonProperty("vault_method")]
		public bool? VaultMethod { get; set; }

		[JsonProperty("make_default")]
		public bool? MakeDefault { get; set; }

		[JsonProperty("receipt_email")]
		public string ReceiptEmail { get; set; }

		[JsonProperty("splits")]
		public IList<ChargeSplit> splits { get; set; }

		protected override string EntityId() {
			return "ChargeRequest";
			// this is only used for json heading in ToString()
		}
		
	}

}
