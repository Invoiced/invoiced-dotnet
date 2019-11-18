using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Task : Entity<Task>
	{

		public Task(Connection conn) : base(conn) {
		}

		public Task() : base(){

		}

		override public long EntityId() {
			return this.Id;
		}

		override public string EntityIdString() {
			return this.Id.ToString();
		}

		override public string EntityName() {
			return "tasks";
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

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("action")]
		public string Action { get; set; }

		[JsonProperty("customer_id")]
		public long CustomerId { get; set; }

        [JsonProperty("user_id")]
		public long UserId { get; set; }

		[JsonProperty("created_at")]
		public long CreatedAt { get; set; }

        [JsonProperty("metadata")]
		public Metadata Metadata { get; set; }
	}

}