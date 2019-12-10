using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Contact : AbstractEntity<Contact>
	{

		private long CustomerId;

		public Contact(Connection conn, long customerId) : base(conn) {
			this.CustomerId = customerId;
		}

		public Contact() : base(){

		}

		protected override string EntityId() {
			return this.Id.ToString();
		}

		public override string EntityName() {
			return "customers/" + CustomerId.ToString() + "/contacts";
		}

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("object")]
		public string Obj { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("primary")]
		public bool Primary { get; set; }

        [JsonProperty("address1")]
		public string Address1 { get; set; }

        [JsonProperty("address2")]
		public string Address2 { get; set; }

        [JsonProperty("city")]
		public string City { get; set; }

        [JsonProperty("state")]
		public string State { get; set; }

        [JsonProperty("postal_code")]
		public string PostalCode { get; set; }

        [JsonProperty("country")]
		public string Country { get; set; }

		[JsonProperty("created_at")]
		public long CreatedAt { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("phone")]
		public string Phone { get; set; }

		[JsonProperty("sms_enabled")]
		public bool SmsEnabled { get; set; }

		[JsonProperty("department")]
		public string Department { get; set; }

		// Conditional Serialisation

		public bool ShouldSerializeId() {
			return false;
		}

		public bool ShouldSerializeObj() {
			return false;
		}

		public bool ShouldSerializeCreatedAt() {
			return false;
		}

	}

}