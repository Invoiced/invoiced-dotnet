using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Note : Entity<Note>
	{

		public Note(Connection conn) : base(conn) {
		}

		public Note() : base(){

		}

		override public long EntityId() {
			return this.Id;
		}

		override public string EntityIdString() {
			return this.Id.ToString();
		}

		override public string EntityName() {
			return "notes";
		}

		override public bool HasCRUD() {
			return true;
		}

		override public bool HasList() {
			return true;
		}

		override public bool HasStringId() {
			return false;
		}

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("object")]
		public string Obj { get; set; }

		[JsonProperty("notes")]
		public string notes { get; set; }

        [JsonProperty("customer")]
		public long Customer { get; set; }

		[JsonProperty("customer_id")]
		public long CustomerId { get; set; }

        [JsonProperty("invoice_id")]
		public long InvoiceId { get; set; }

		[JsonProperty("created_at")]
		public long CreatedAt { get; set; }

        [JsonProperty("metadata")]
		public Metadata Metadata { get; set; }
	}

}
