using System;
using System.Collections.Generic;
using System.Transactions;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Note : AbstractEntity<Note>
	{

		public Note(Connection conn) : base(conn) {
			this.EntityName = "/notes";
		}

		public Note() : base(){
			this.EntityName = "/notes";
		}

		protected override string EntityId() {
			return this.Id.ToString();
		}

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("object")]
		public string Obj { get; set; }

		[JsonProperty("notes")]
		public string Notes { get; set; }

        [JsonProperty("customer")]
		public long Customer { get; set; }

		[JsonProperty("customer_id")]
		public long CustomerId { get; set; }

        [JsonProperty("invoice_id")]
		public long? InvoiceId { get; set; }

		[JsonProperty("created_at")]
		public long CreatedAt { get; set; }

		public override void Create() {
			String endpointBase = this.GetEndpointBase();
			this.SetEndpointBase("");
			try {
				base.Create();
			}
			catch {
				this.SetEndpointBase(endpointBase);
				throw;
			}
			this.SetEndpointBase(endpointBase);
		}
		
		public override void SaveAll() {
			String endpointBase = this.GetEndpointBase();
			this.SetEndpointBase("");
			try {
				base.SaveAll();
			}
			catch {
				this.SetEndpointBase(endpointBase);
				throw;
			}
			this.SetEndpointBase(endpointBase);
		}
		
		public override void Delete() {
			String endpointBase = this.GetEndpointBase();
			this.SetEndpointBase("");
			try {
				base.Delete();
			}
			catch {
				this.SetEndpointBase(endpointBase);
				throw;
			}
			this.SetEndpointBase(endpointBase);
		}

		// Conditional Serialisation

		public bool ShouldSerializeId() {
			return false;
		}

		public bool ShouldSerializeObj() {
			return false;
		}

		public bool ShouldSerializeCustomer() {
			return false;
		}

		public bool ShouldSerializeCustomerId() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeInvoiceId() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeCreatedAt() {
			return false;
		}
			
	}

}
