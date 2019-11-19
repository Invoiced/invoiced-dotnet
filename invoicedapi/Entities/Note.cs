using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Note : AbstractEntity<Note>
	{

		public Note(Connection conn, long CustomerId, long InvoiceId) : base(conn) {
			// cannot set all the time; this avoids SaveAll() error
			if(CustomerId > 0) this.CustomerId = CustomerId;
			if(InvoiceId > 0) this.InvoiceId = InvoiceId;
		}

		public Note() : base(){

		}

		public override long EntityId() {
			return this.Id;
		}

		public override string EntityIdString() {
			return this.Id.ToString();
		}

		public override string EntityName() {
			return "notes";
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
		public long InvoiceId { get; set; }

		[JsonProperty("created_at")]
		public long CreatedAt { get; set; }

		// Retrieve() for notes must produce a list and also account for two possible endpoints
		public new IList<Note> Retrieve() {

			string url = null;

			if(this.CustomerId > 0) {
				url = this.Connection.baseUrl() + "/customers/" + this.CustomerId + "/notes";
			} else if(this.InvoiceId > 0) {
				url = this.Connection.baseUrl() + "/invoices/" + this.InvoiceId + "/notes";
			} else {
				return null;
			}

			ListResponse response = this.Connection.GetList(url,null);

			EntityList<Note> entities;
			
			try {
					entities = JsonConvert.DeserializeObject<EntityList<Note>>(response.Result,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
					entities.LinkURLS = response.Links;
					entities.TotalCount = response.TotalCount;
			} catch(Exception e) {
				throw new EntityException("",e);
			}

			foreach (var entity in entities) {
				entity.ChangeConnection(Connection);
			}

			return entities;

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
			if (this.CurrentOperation != "Create") return false;
			return true;
		}

		public bool ShouldSerializeInvoiceId() {
			if (this.CurrentOperation != "Create") return false;
			return true;
		}

		public bool ShouldSerializeCreatedAt() {
			return false;
		}
			
	}

}
